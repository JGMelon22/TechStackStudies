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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddNewTechnologyAsync([FromBody] TechnologyRequest newTechnology)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(newTechnology);
        if (!validationResult.IsValid)
            return BadRequest(string.Join(',', validationResult.Errors));

        ServiceResponse<bool> technology = await _messageBus.InvokeAsync<ServiceResponse<bool>>(new AddTechnologyCommand(newTechnology));
        return technology.Data != false
            ? Ok(technology)
            : BadRequest(technology);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllTechnologiesAsync([FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        ServiceResponse<PagedResponseOffset<TechnologyResponse>> technologies =
            await _messageBus.InvokeAsync<ServiceResponse<PagedResponseOffset<TechnologyResponse>>>(
                new GetTechnologiesQuery(pageNumber, pageSize));

        return technologies.Data != null && technologies.Data.Data.Any()
          ? Ok(technologies)
          : NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetTechnologyByIdAsync([FromRoute] int id)
    {
        ServiceResponse<TechnologyResponse> technology = await _messageBus.InvokeAsync<ServiceResponse<TechnologyResponse>>(new GetTechnologyByIdQuery(id));
        return technology.Data != null
            ? Ok(technology)
            : NotFound(technology);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTechnologyAsync([FromRoute] int id, [FromBody] TechnologyRequest updatedTechnology)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(updatedTechnology);
        if (!validationResult.IsValid)
            return BadRequest(string.Join(',', validationResult.Errors));

        ServiceResponse<bool> technology = await _messageBus.InvokeAsync<ServiceResponse<bool>>(new UpdateTechnologyCommand(id, updatedTechnology));
        return technology.Success != false
            ? Ok(technology)
            : BadRequest(technology);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveTechnologyAsync([FromRoute] int id)
    {
        ServiceResponse<bool> technology = await _messageBus.InvokeAsync<ServiceResponse<bool>>(new RemoveTechnologyCommand(id));
        return technology.Success != false
            ? NoContent()
            : BadRequest(technology);
    }
}
