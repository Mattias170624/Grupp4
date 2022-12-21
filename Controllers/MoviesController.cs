using System;
using Microsoft.AspNetCore.Mvc;
using Grupp4.Services;
using Grupp4.Models;

namespace Grupp4.Controllers;

[Controller]
[Route("api/[controller]")]
public class MoviesController: Controller {

    private readonly MoviesDBService _moviesDBService;

    public MoviesController(MoviesDBService mongoDBService){
        _moviesDBService = mongoDBService;
    }

    /// <summary>
    /// Gets all the movies from the list
    /// </summary>
    [HttpGet]
    public async Task<List<Movies>> Get() {
        return await _moviesDBService.GetAsync();
    }

    /// <summary>
    /// Gets a specific movie from the list from a specific id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Movies>> Get(string movieId) {
        var movies = await _moviesDBService.GetAsyncId(movieId);
        if (movies == null) {
            return NotFound();
        }
        return movies;
    }

    /// <summary>
    /// Creates a movie to add to the list
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Movies movies) {
        await _moviesDBService.CreateAsync(movies);
        return CreatedAtAction(nameof(Get), new { id = movies.Id}, movies);
    }

    /// <summary>
    /// Adds a movie to add to the list
    /// </summary>
    [HttpPut("{id}")]
    public  async Task<IActionResult> AddToMovies(string id, [FromBody] string movieId) {
        await _moviesDBService.AddToMoviesAsync(id, movieId);
        return NoContent();
    }

    /// <summary>
    /// Deletes a movie from the list
    /// </summary>
    [HttpDelete("{id}")]
    public  async Task<IActionResult> Delete(string id) {
        await _moviesDBService.DeleteAsync(id);
        return NoContent();
    }
}