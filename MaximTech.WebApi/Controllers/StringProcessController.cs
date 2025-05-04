using MaximTech.Application.Services;
using MaximTech.Domain.Models;
using MaximTech.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MaximTech.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StringProcessController : ControllerBase
{
    private readonly StringProcessService _stringProcessService;

    public StringProcessController(StringProcessService stringProcessService)
    {
        _stringProcessService = stringProcessService;
    }

    [HttpGet]
    public async Task<IActionResult> ProcessString(string input, SortType sortType = SortType.QuickSort)
    {
        try
        {
            var result = await _stringProcessService.ProcessString(input, sortType);
            var response = new StringProcessResult
            (
                result.reversed,
                result.repetitions,
                result.substringInBand,
                result.sorted,
                result.trimmed
            );

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}