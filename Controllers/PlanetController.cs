using System;
using Microsoft.AspNetCore.Mvc;
using Grupp4.Models;
using Grupp4.Services;

namespace Grupp4.Controllers;

#pragma warning disable CS1591
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PlanetController: Controller {

    private readonly PlanetDBService _planetDBService;

    public PlanetController(PlanetDBService planetDBService) {
        _planetDBService = planetDBService;  
    }

    /// <summary>
    /// Gets all the items from the list.
    /// </summary>
    [HttpGet]
    public async Task<List<Planets>> Get() {
        return await _planetDBService.GetAsync();
    }
    /// <summary>
    /// Gets a specific item from the list.
    /// </summary>
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Planets>> Get(string id)
    {
        var planet = await _planetDBService.GetAsync(id);

        if (planet is null)
        {
            return NotFound();
        }

        return planet;
    }

    /// <summary>
    /// Creates an item.
    /// </summary>
    /// <param name="planet"></param>
    /// <returns>New planet created.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Planet
    ///     {
    ///        "name": "Neptune",
    ///        "orderFromSun": 8,
    ///        "hasRings": true,
    ///        "mainAtmosphere": [
    ///         "hydrogen"
    ///        ],
    ///        "surfaceTemperatureC": {
    ///         "min": -100,
    ///         "max": 100,
    ///         "mean": 50    
    ///        }
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] Planets planets) {
        await _planetDBService.CreateAsync(planets);
        return CreatedAtAction(nameof(Get), new {id = planets._id}, planets);
    }

    /// <summary>
    /// Updates an item from the list.
    /// </summary>
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

    /// <summary>
    /// Deletes a specific item from the list.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _planetDBService.DeleteAsync(id);
        return NoContent();
    }
}
#pragma warning restore CS1591