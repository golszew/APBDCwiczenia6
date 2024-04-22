using Tutorial5.Models;
using Tutorial5.Models.DTOs;

namespace Tutorial5.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals();
    void AddAnimal(AddAnimal animal);

    void UpdateAnimal(int id, AddAnimal animal);

    void RemoveAnimal(int id);
}