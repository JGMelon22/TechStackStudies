using TechStackStudies.Application.Commands;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;

namespace TechStackStudies.Application.Handlers;

public class UpdateTechnologyCommandHandler 
{
    private readonly ITechnologyRepository _technologyRepository;

    public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<ServiceResponse<TechnologyResponse>> Handle(UpdateTechnologyCommand command)
        => await _technologyRepository.UpdateTechnologyAsync(command.Id, command.UpdatedTechnology);
}
