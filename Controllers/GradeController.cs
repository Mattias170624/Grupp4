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

    [HttpGet]
    public async Task<List<GradeModel>> Get2()
    {
        return await _gradeService.GetAsync2();
    }

    [HttpGet("{id}")]
    public async Task<GradeModel?> GetSpecificGrade(string id)
    {
        return await _gradeService.GetById(id);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GradeModel grade)
    {
        await _gradeService.CreateAsync(grade);
        return CreatedAtAction(nameof(Get2), new { id = grade.Id }, grade);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] ScoreModel score, string id)
    {
        await _gradeService.AddScoreAsync(id, score);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _gradeService.DeleteAsync(id);
        return NoContent();
    }
}