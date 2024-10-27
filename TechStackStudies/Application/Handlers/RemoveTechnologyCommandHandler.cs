using TechStackStudies.Application.Commands;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;

namespace TechStackStudies.Application.Handlers;

public class RemoveTechnologyCommandHandler
{
    private readonly ITechnologyRepository _technologyRepository;

    public RemoveTechnologyCommandHandler(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<ServiceResponse<bool>> Handle(RemoveTechnologyCommand command)
        => await _technologyRepository.RemoveTechnologyAsync(command.Id);
}
