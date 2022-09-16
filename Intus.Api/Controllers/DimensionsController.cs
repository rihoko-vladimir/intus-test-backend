using Intus.Business.Interfaces.Services;
using Intus.Infrastructure.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Intus.Api.Controllers;

[ApiController]
[Route("/api/dimensions")]
public class CanvasController : ControllerBase
{
    private readonly IDimensionsService _dimensionsService;

    public CanvasController(IDimensionsService dimensionsService)
    {
        _dimensionsService = dimensionsService;
    }

    [HttpGet]
    public async Task<ActionResult> GetDimensions()
    {
        return Ok(await _dimensionsService.GetDimensionsAsync());
    }

    [HttpPost]
    public async Task<ActionResult> PostNewDimensions([FromBody] RectangleDimensionsRequest request)
    {
        await _dimensionsService.PushDimensionsAsync(request);
        return Ok();
    }
}