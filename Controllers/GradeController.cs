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

    /// <summary> Returns all grade objects inside the collection </summary>
    [HttpGet]
    public async Task<List<GradeModel>> Get()
    {
        return await _gradeService.GetAsync();
    }

    /// <summary> Returns a specific grade object using object id </summary>
    [HttpGet("{id}")]
    public async Task<GradeModel?> GetSpecificGrade(string id)
    {
        return await _gradeService.GetById(id);
    }

    /// <summary> Adds a new grade object to the collection </summary>
    /// <response code="201"> Success </response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GradeModel grade)
    {
        await _gradeService.CreateAsync(grade);
        return CreatedAtAction(nameof(Get), new { id = grade.Id }, grade);
    }

    /// <summary> Adds a score object to a specific grade using object id </summary>
    /// <param name="id"> The object ID of the grade object to edit </param>
    /// <response code="201"> Success </response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] ScoreModel score, string id)
    {
        await _gradeService.AddScoreAsync(id, score);
        return NoContent();
    }

    /// <summary> Deletes specific grade using object id </summary>
    /// <param name="id"> The object ID of the grade object to delete </param>
    /// <response code="204"> Success </response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _gradeService.DeleteAsync(id);
        return NoContent();
    }
}