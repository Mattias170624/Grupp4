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

    [HttpGet]
    public async Task<List<Movies>> Get() {
        return await _moviesDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Movies movies) {
        await _moviesDBService.CreateAsync(movies);
        return CreatedAtAction(nameof(Get), new { id = movies.Id}, movies);
    }


    [HttpPut("{id}")]
    public  async Task<IActionResult> AddToMovies(string id, [FromBody] string movieId) {
        await _moviesDBService.AddToMoviesAsync(id, movieId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public  async Task<IActionResult> Delete(string id) {
        await _moviesDBService.DeleteAsync(id);
        return NoContent();
    }

    




}