using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetshopAPI.Context;
using PetshopAPI.Models;

namespace PetshopAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PetController:ControllerBase
    {
        private readonly PetDbContext _context;

        public PetController(PetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPet()
        {
            return Ok(new { success = true, data = await _context.Pets.ToListAsync() });
        }
        [HttpPost]
        public async Task<IActionResult> CreatePet(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, data = pet});
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(int id, Pet pet)
        {
            var x = await _context.Pets.FindAsync(id);
            if (x == null)
            {
                return NotFound();
            }

            x.Name = pet.Name;
            x.Raca = pet.Raca;
            x.Idade = pet.Idade;
            x.Genero = pet.Genero;

            _context.Pets.Update(x);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, data = x });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}
