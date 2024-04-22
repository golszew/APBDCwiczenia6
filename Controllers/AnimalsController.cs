using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;
using Tutorial5.Repositories;

namespace Tutorial5.Controllers;

[ApiController]
// [Route("api/[controller]")]
[Route("api/animals")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;
    
    public AnimalsController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    [HttpGet]
    public IActionResult GetAnimals()
    {
        var animals = _animalRepository.GetAnimals();

        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        _animalRepository.AddAnimal(animal);
        
        return Created("", null);
    }
    
    [HttpGet("{orderBy}")]
    public IActionResult GetAnimals(string orderBy)
    {
        var animals = _animalRepository.GetAnimals();
        switch (orderBy.ToLower())
        {
            case "name":
                animals = animals.OrderBy(animal => animal.Name);
                break;
            case "description":
                animals = animals.OrderBy(animal => animal.Description);
                break;
            case "category":
                animals = animals.OrderBy(animal => animal.Category);
                break;
            case "area":
                animals = animals.OrderBy(animal => animal.Area);
                break;
            default:
                animals = animals.OrderBy(animal => animal.Name);
                break;
        }
        return Ok(animals);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, AddAnimal animal)
    {
        var animalToEdit = _animalRepository.GetAnimals().FirstOrDefault(anm => anm.IdAnimal == id);
        if (animalToEdit == null)
            return NotFound($"Animal with id {id} was not found");
        
        _animalRepository.UpdateAnimal(id, animal);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animalToEdit = _animalRepository.GetAnimals().FirstOrDefault(anm => anm.IdAnimal == id);
        if (animalToEdit == null)
            return NoContent();
        
        _animalRepository.RemoveAnimal(id);
        return NoContent();
    }
    
    
}