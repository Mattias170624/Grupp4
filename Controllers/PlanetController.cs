using System;
using Microsoft.AspNetCore.Mvc;
using Grupp4.Models;
using Grupp4.Services;

namespace Grupp4.Controllers;

[Controller]
[Route("api/[controller]")]
public class PlanetController: Controller {

    private readonly PlanetDBService _mongoDBService;

    public PlanetController(PlanetDBService mongoDBService) {
        _mongoDBService = mongoDBService;  
    }

    [HttpGet]
    public async Task<List<Planets>> Get() {
        return await _mongoDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Planets playlist) {
        await _mongoDBService.CreateAsync(playlist);
        return CreatedAtAction(nameof(Get), new {id = playlist.Id}, playlist);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) {
        await _mongoDBService.AddToPlaylistAsync(id, movieId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }
}
