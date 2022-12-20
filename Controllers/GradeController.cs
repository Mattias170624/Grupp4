using Microsoft.AspNetCore.Mvc;
using Grupp4.Models;
using Grupp4.Services;

namespace Grupp4.Controllers;

[Controller]
[Route("api/[controller]")]
public class GradeController : Controller
{
    private readonly GradeService _gradeService;

    public GradeController(GradeService gradeService)
    {
        _gradeService = gradeService;
    }

    /// <summary>
    /// Gets all the items from the list.
    /// </summary>
    [HttpGet]
    public async Task<List<GradeModel>> Get2()
    {
        return await _gradeService.GetAsync2();
    }

    /// <summary>
    /// Gets a specific item from the list.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<GradeModel?> GetSpecificGrade(string id)
    {
        return await _gradeService.GetById(id);
    }

    /// <summary>
    /// Creates an item.
    /// </summary>
    /// <param name="grade"></param>
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
    public async Task<IActionResult> Post([FromBody] GradeModel grade)
    {
        await _gradeService.CreateAsync(grade);
        return CreatedAtAction(nameof(Get2), new { id = grade.Id }, grade);
    }

    /// <summary>
    /// Updates an item from the list.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] ScoreModel score, string id)
    {
        await _gradeService.AddScoreAsync(id, score);
        return NoContent();
    }

    /// <summary>
    /// Updates an item from the list.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _gradeService.DeleteAsync(id);
        return NoContent();
    }
}