using TechStackStudies.Application.Commands;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;

namespace TechStackStudies.Application.Handlers;

public class AddTechnologyCommandHandler
{
    private readonly ITechnologyRepository _technologyRepository;

    public AddTechnologyCommandHandler(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<ServiceResponse<TechnologyResponse>> Handle(AddTechnologyCommand command)
        => await _technologyRepository.AddTechnologyAsync(command.NewTechnology);
}
