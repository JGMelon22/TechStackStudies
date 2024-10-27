using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TechStackStudies.Application.Commands;
using TechStackStudies.Application.Queries;
using TechStackStudies.DTOs;
using TechStackStudies.Models;
using Wolverine;

namespace TechStackStudies.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TechnologiesController : ControllerBase
{
    private readonly IValidator<TechnologyRequest> _validator;
    private readonly IMessageBus _messageBus;

    public TechnologiesController(IValidator<TechnologyRequest> validator, IMessageBus messageBus)
    {
        _validator = validator;
        _messageBus = messageBus;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddNewTechnologyAsync([FromBody] TechnologyRequest newTechnology)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(newTechnology);
        if (!validationResult.IsValid)
            return BadRequest(string.Join(',', validationResult.Errors));

        ServiceResponse<TechnologyResponse> technology = await _messageBus.InvokeAsync<ServiceResponse<TechnologyResponse>>(new AddTechnologyCommand(newTechnology));
        return technology.Data != null
            ? Ok(technology)
            : BadRequest(technology);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllTechnologiesAsync()
    {
        ServiceResponse<IEnumerable<TechnologyResponse>> technologies = await _messageBus.InvokeAsync<ServiceResponse<IEnumerable<TechnologyResponse>>>(new GetTechnologiesQuery());
        return technologies.Data != null && technologies.Data.Any()
            ? Ok(technologies)
            : NoContent();
    }
}
