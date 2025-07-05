using TechStackStudies.Application.Commands;
using TechStackStudies.DTOs;
using TechStackStudies.Infrastructure.Mappers;
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

    public async Task<ServiceResponse<bool>> Handle(UpdateTechnologyCommand command)
    {
        Technology technology = TechnologyMapper.ToDomain(command.UpdatedTechnology);
        ServiceResponse<bool> result = await _technologyRepository.UpdateTechnologyAsync(command.Id, technology);

        return new ServiceResponse<bool>
        {
            Success = result.Success,
            Message = result.Message,
            Data = result.Data
        };
    }
}
