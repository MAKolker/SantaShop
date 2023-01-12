using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SantaShop.Api.Extensions;
using SantaShop.Domain.Dto;
using SantaShop.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SantaShop.Api.Controllers;

/// <summary>
/// Gift Request api controller
/// </summary>
[ApiController]
public class GiftRequestController: ControllerBase
{
    private readonly ILogger<GiftRequestController> _logger;
    private readonly IValidator<GiftRequest> _validator;
    private readonly IGiftRequestService _service;

    public GiftRequestController(ILogger<GiftRequestController> logger, IValidator<GiftRequest> validator, IGiftRequestService service)
    {
        _logger = logger;
        _validator = validator;
        _service = service;
    }

        /// <summary>
        /// Create gift request
        /// </summary>
        /// <param name="body">Gift request</param>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost]
        [Route("/giftrequest")]
        [SwaggerOperation("CreateGiftRequest")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
        public virtual async Task<IActionResult> CreateGiftRequest([FromBody] GiftRequest request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request);

            var result = await _service.CreateGiftRequestAsync(request, token);

            if (!result.HasErrors)
                return Ok(result.Object);
            return BadRequest(result.ErrorMessage);
        } 
        
        
        /// <summary>
        /// Create or update gift request
        /// </summary>
        /// <param name="body">Gift request</param>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPut]
        [Route("/giftrequest")]
        [SwaggerOperation("CreateGiftRequest")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public virtual async Task<IActionResult> CreateOrUpdateGiftRequest([FromBody] GiftRequest request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request);

            var result = await _service.CreateOrUpdateGiftRequestAsync(request, token);

            if (!result.HasErrors)
                return Ok(result.Object);
            return BadRequest(result.ErrorMessage);
        }
   
   
}