using System;
using Microsoft.AspNetCore.Mvc;
using Grupp4.Services;
using Grupp4.Models;

namespace Grupp4.Controllers; 

[Controller]
[Route("api/[controller]")]
public class PlaylistController: Controller {

    private readonly MongoDbService _mongoDbService;

    public PlaylistController(MongoDbService mongoDbService) {
        _mongoDbService = mongoDbService;
    }

    [HttpGet]
    public async Task<List<Playlist>> Get() {
        return await _mongoDbService.GetAsync();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Playlist playlist) {
        await _mongoDbService.CreateAsync(playlist);
        return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Playlist>> Get(string id) {
        var playlist = await _mongoDbService.GetAsyncId(id);
        if (playlist == null) {
            return NotFound();
        }
        return playlist;
    }
    

    [HttpPut("{id}")]

    public async Task<ActionResult> AddToPlaylist(string id, [FromBody] string Items) {
        await _mongoDbService.AddToPlaylistAsync(id, Items);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id) {
        await _mongoDbService.DeleteAsync(id);
        return NoContent();
    }

}