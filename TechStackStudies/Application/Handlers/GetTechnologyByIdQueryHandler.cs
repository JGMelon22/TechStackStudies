using TechStackStudies.Application.Queries;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;

namespace TechStackStudies.Application.Handlers;

public class GetTechnologyByIdQueryHandler
{
    private readonly ITechnologyRepository _technologyRepository;

    public GetTechnologyByIdQueryHandler(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<ServiceResponse<TechnologyResponse>> Handle(GetTechnologyByIdQuery query)
        => await _technologyRepository.GetTechnologyByIdAsync(query.Id);
}
