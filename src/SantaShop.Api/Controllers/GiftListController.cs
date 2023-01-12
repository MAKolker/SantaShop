using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SantaShop.Domain.Dto;
using SantaShop.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SantaShop.Api.Controllers;

/// <summary>
/// Gift List api controller
/// </summary>
[ApiController]
public class GiftListController:ControllerBase
{
    private readonly ILogger<GiftListController> _logger;
    private readonly IGiftListService _service;

    public GiftListController(ILogger<GiftListController> logger, IGiftListService service)
    {
        _logger = logger;
        _service = service;
    }
    
    /// <summary>
    /// Get Gift List
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not found</response>
    [HttpGet]
    [Route("/giftlist")]
    [SwaggerOperation("GetGiftList")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerResponse(statusCode: 200, type: typeof(List<GiftList>), description: "Success")]
    public virtual async Task<IActionResult> GetGiftList()
    {
        var result = await _service.GetGiftListAsync();
        if (!result.HasErrors)
            return Ok(result.Object);
        return BadRequest(result.ErrorMessage);
    }
}