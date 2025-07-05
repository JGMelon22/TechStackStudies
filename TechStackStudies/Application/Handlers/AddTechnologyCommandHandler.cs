using TechStackStudies.Application.Commands;
using TechStackStudies.Infrastructure.Mappers;
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

    public async Task<ServiceResponse<bool>> Handle(AddTechnologyCommand command)
    {
        Technology technology = TechnologyMapper.ToDomain(command.NewTechnology);
        ServiceResponse<bool> result = await _technologyRepository.AddTechnologyAsync(technology);

        return new ServiceResponse<bool>
        {
            Success = result.Success,
            Message = result.Message,
            Data = result.Data
        };
    }
}
