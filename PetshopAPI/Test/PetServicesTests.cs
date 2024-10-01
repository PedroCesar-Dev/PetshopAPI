﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetshopAPI.Context;
using PetshopAPI.Controllers;
using PetshopAPI.Models;
using System.Threading.Tasks;
using Xunit;

public class PetControllerTests
{
    private PetDbContext GetInMemoryPetDbContext()
    {
        var options = new DbContextOptionsBuilder<PetDbContext>()
            .UseInMemoryDatabase(databaseName: "PetDatabase")
            .Options;
        return new PetDbContext(options);
    }

    [Fact]
    public async Task GetPet_ReturnsHttp200()
    {
        // Arrange
        var context = GetInMemoryPetDbContext();
        context.Pets.Add(new Pet { Name = "Buddy", Raca = "Golden Retriever", Idade = 3, Genero = "Male" });
        context.Pets.Add(new Pet { Name = "Max", Raca = "Labrador", Idade = 2, Genero = "Male" });
        await context.SaveChangesAsync();

        var controller = new PetController(context);

        // Act
        var result = await controller.GetPet();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task CreatePet_ReturnsHttp200()
    {
        // Arrange
        var context = GetInMemoryPetDbContext();
        var controller = new PetController(context);

        var newPet = new Pet { Id = 3, Name = "Charlie", Raca = "Poodle", Idade = 4, Genero = "Male" };

        // Act
        var result = await controller.CreatePet(newPet);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task UpdatePet_ReturnsHttp200_WhenPetExists()
    {
        // Arrange
        var context = GetInMemoryPetDbContext();
        var existingPet = new Pet { Id = 1, Name = "Buddy", Raca = "Golden Retriever", Idade = 3, Genero = "Male" };
        context.Pets.Add(existingPet);
        await context.SaveChangesAsync();

        var controller = new PetController(context);

        var updatedPet = new Pet { Name = "UpdatedName", Raca = "UpdatedRaca", Idade = 5, Genero = "Female" };

        // Act
        var result = await controller.UpdatePet(1, updatedPet);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task DeletePet_ReturnsHttp200_WhenPetExists()
    {
        // Arrange
        var context = GetInMemoryPetDbContext();
        var pet = new Pet { Id = 1, Name = "Buddy", Raca = "Golden Retriever", Idade = 3, Genero = "Male" };
        context.Pets.Add(pet);
        await context.SaveChangesAsync();

        var controller = new PetController(context);

        // Act
        var result = await controller.DeletePet(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}

//test