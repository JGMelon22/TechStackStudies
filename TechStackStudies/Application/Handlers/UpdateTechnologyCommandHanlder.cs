using TechStackStudies.Application.Commands;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;

namespace TechStackStudies.Application.Handlers;

public class UpdateTechnologyCommandHanlder
{
    private readonly ITechnologyRepository _technologyRepository;

    public UpdateTechnologyCommandHanlder(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<ServiceResponse<TechnologyResponse>> Handle(UpdateTechnologyCommand command)
        => await _technologyRepository.UpdateTechnologyAsync(command.Id, command.UpdatedTechnology);
}
