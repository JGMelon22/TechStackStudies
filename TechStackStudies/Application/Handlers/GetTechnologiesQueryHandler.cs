using TechStackStudies.Application.Queries;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;

namespace TechStackStudies.Application.Handlers;

public class GetTechnologiesQueryHandler
{
    private readonly ITechnologyRepository _technologyRepository;

    public GetTechnologiesQueryHandler(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<ServiceResponse<IEnumerable<TechnologyResponse>>> Handle(GetTechnologiesQuery query)
        => await _technologyRepository.GetAllTechnologiesAsync();
}
