using TechStackStudies.Application.Queries;
using TechStackStudies.DTOs;
using TechStackStudies.Infrastructure.Mappers;
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
    {
        ServiceResponse<IEnumerable<Technology>> result = await _technologyRepository.GetAllTechnologiesAsync();

        return new ServiceResponse<IEnumerable<TechnologyResponse>>
        {
            Success = result.Success,
            Message = result.Message,
            Data = TechnologyMapper.ToResponse(result.Data!)
        };
    }
}
