using PetshopAPI.Models;

namespace PetshopAPI.Interfaces
{
    public interface IPetRepository
    {
        Pet GetPetById(int id);
        void AddPet(Pet pet);
        void DeletePetById(int id);
        List<Pet> GetAllPets();
    }
}
