using System;
using Microsoft.AspNetCore.Mvc;
using Grupp4.Models;
using Grupp4.Services;

namespace Grupp4.Controllers;

[Controller]
[Route("api/[controller]")]
public class PlanetController: Controller {

    private readonly PlanetDBService _planetDBService;

    public PlanetController(PlanetDBService planetDBService) {
        _planetDBService = planetDBService;  
    }

    [HttpGet]
    public async Task<List<Planets>> Get() {
        return await _planetDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Planets planets) {
        await _planetDBService.CreateAsync(planets);
        return CreatedAtAction(nameof(Get), new {id = planets.Id}, planets);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToPlanets(string id, [FromBody] string planetId) {
        await _planetDBService.AddToPlanetsAsync(id, planetId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _planetDBService.DeleteAsync(id);
        return NoContent();
    }
}
