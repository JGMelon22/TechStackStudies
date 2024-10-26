using TechStackStudies.DTOs;
using TechStackStudies.Models;

namespace TechStackStudies.Interfaces;

public interface ITechnologyRepository
{
    Task<ServiceResponse<IEnumerable<TechnologyResponse>>> GetAllTechnologiesAsync();
    Task<ServiceResponse<TechnologyResponse>> GetTechnologyByIdAsync(int id);
    Task<ServiceResponse<TechnologyResponse>> AddTechnologyAsync(TechnologyRequest newTechnology);
    Task<ServiceResponse<TechnologyResponse>> UpdateTechnologyAsync(int id, TechnologyRequest newTechnology);
    Task<ServiceResponse<bool>> RemoveTechnologyAsync(int id);
}
