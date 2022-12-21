using System;
using Microsoft.AspNetCore.Mvc;
using Grupp4.Services;
using Grupp4.Models;

namespace Grupp4.Controllers; 

[Controller]
[Route("api/[controller]")]
public class PlaylistController: Controller {

    private readonly PlaylistDBService _PlaylistDBService;

    public PlaylistController(PlaylistDBService playlistDBServic) {
        _PlaylistDBService = playlistDBServic;
    }

    /// <summary>Get all playlists</summary>
    [HttpGet]
    public async Task<List<Playlist>> Get() {
        return await _PlaylistDBService.GetAsync();
    }

    /// <summary>Create a playlist</summary>
    /// <param name="Playlist"></param>
    /// <returns>New playlist created.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /playlist
    ///     {
    ///        "id": "string",     
    ///        "username": "string",
    ///        "items": [
    ///         "string"         
    ///        ]
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created playlist</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] Playlist playlist) {
        await _PlaylistDBService.CreateAsync(playlist);
        return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);
    }

    /// <summary>Get a specific playlist from an Id</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Playlist>> Get(string id) {
        var playlist = await _PlaylistDBService.GetAsyncId(id);
        if (playlist == null) {
            return NotFound();
        }
        return playlist;
    }
    
    /// <summary>Add a movie to a playlist</summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> AddToPlaylist(string id, [FromBody] string Items) {
        await _PlaylistDBService.AddToPlaylistAsync(id, Items);
        return NoContent();
    }

    /// <summary>Delete a playlist</summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id) {
        await _PlaylistDBService.DeleteAsync(id);
        return NoContent();
    }

}