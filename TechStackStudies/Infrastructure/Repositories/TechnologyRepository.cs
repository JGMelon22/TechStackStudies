using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;

namespace TechStackStudies.Infrastructure.Repositories;

public class TechnologyRepository : ITechnologyRepository
{
    public Task<ServiceResponse<TechnologyResponse>> AddTechnologyAsync(TechnologyRequest newTechnology)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<IEnumerable<TechnologyResponse>>> GetAllTechnologiesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<TechnologyResponse>> GetTechnologyByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<bool>> RemoveTechnologyAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<TechnologyResponse>> UpdateTechnologyAsync(int id, TechnologyRequest newTechnology)
    {
        throw new NotImplementedException();
    }
}
