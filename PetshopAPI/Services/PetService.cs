using PetshopAPI.Interfaces;
using PetshopAPI.Models;

namespace PetshopAPI.Services
{
    public class PetService
    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }
        public Pet GetPetById(int id)
        {
            return _petRepository.GetPetById(id);
        }
        public void AddPet(Pet pet)
        {
            _petRepository.AddPet(pet);
        }
        public void DeletePetById(int id)
        {
            _petRepository.DeletePetById(id);
        }
        public List<Pet> GetAllPets()
        {
            return _petRepository.GetAllPets();
        }
    }
}
