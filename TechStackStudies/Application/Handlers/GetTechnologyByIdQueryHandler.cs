using TechStackStudies.Application.Queries;
using TechStackStudies.DTOs;
using TechStackStudies.Infrastructure.Mappers;
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
    {
        ServiceResponse<Technology> result = await _technologyRepository.GetTechnologyByIdAsync(query.Id);

        return new ServiceResponse<TechnologyResponse>
        {
            Success = result.Success,
            Message = result.Message,
            Data = TechnologyMapper.ToResponse(result.Data!)
        };
    }
}
