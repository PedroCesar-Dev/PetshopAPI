using Moq;
using PetshopAPI.Interfaces;
using PetshopAPI.Models;
using PetshopAPI.Services;
using Xunit;

namespace PetshopAPI.Test
{
    public class PetServicesTests
    {
        private readonly Mock<IPetRepository> _petRepositoryMock;
        private readonly PetService _petService;

        public PetServicesTests() 
        {
            _petRepositoryMock = new Mock<IPetRepository>();
            _petService = new PetService(_petRepositoryMock.Object);
        }

        [Fact]
        public void GetPetById()
        {            
            var petId = 1;
            var pet = new Pet { Id = petId, Name = "Bob", Raca = "Pastor alemão", Genero = "M", Idade = 5 };

            _petRepositoryMock.Setup(repo => repo.GetPetById(petId)).Returns(pet);

            var result = _petService.GetPetById(petId);
            Assert.NotNull(result);
            Assert.Equal(petId, result.Id);
            Assert.Equal("Bob", result.Name);
        }

        [Fact]
        public void AddById()
        {
            var pet = new Pet { Id = 2, Name = "Teo", Raca = "Border Collie", Genero = "M", Idade = 10 };

            _petService.AddPet(pet);

            _petRepositoryMock.Verify(repo => repo.AddPet(pet), Times.Once);
        }

        [Fact]
        public void DeleteById()
        {
            var petId = 1;
            
            _petService.DeletePetById(petId);

            _petRepositoryMock.Verify(repo => repo.DeletePetById(petId), Times.Once);
        }

        [Fact]
        public void GetAllPets() 
        {
            var pets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Bob", Raca = "Pastor alemão", Genero = "M", Idade = 5 },
                new Pet { Id = 2, Name = "Teo", Raca = "Border Collie", Genero = "M", Idade = 10 }
            };

            _petRepositoryMock.Setup(repo => repo.GetAllPets()).Returns(pets);

            var result = _petService.GetAllPets();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == "Bob");
        }
    }
}
