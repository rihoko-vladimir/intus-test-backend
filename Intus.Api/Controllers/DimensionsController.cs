using Microsoft.AspNetCore.Mvc;

namespace intus_test_backend.Controllers;

[ApiController]
[Route("/api/dimensions")]
public class CanvasController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetDimensions()
    {
        
    }

    [HttpPost]
    public async Task<ActionResult> PostNewDimensions()
    {
        
    }
}