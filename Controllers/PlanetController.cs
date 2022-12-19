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
        return CreatedAtAction(nameof(Get), new {id = planets._id}, planets);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, [FromBody] Planets updatedPlanet)
    {
        var planet = await _planetDBService.GetAsync(id);

        if (planet is null)
        {
            return NotFound();
        }

        updatedPlanet._id = planet._id;

        await _planetDBService.UpdateAsync(id, updatedPlanet);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _planetDBService.DeleteAsync(id);
        return NoContent();
    }
}
