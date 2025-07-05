using TechStackStudies.Models;

namespace TechStackStudies.Interfaces;

public interface ITechnologyRepository
{
    Task<ServiceResponse<IEnumerable<Technology>>> GetAllTechnologiesAsync();
    Task<ServiceResponse<Technology>> GetTechnologyByIdAsync(int id);
    Task<ServiceResponse<bool>> AddTechnologyAsync(Technology newTechnology);
    Task<ServiceResponse<bool>> UpdateTechnologyAsync(int id, Technology updatedTechnology);
    Task<ServiceResponse<bool>> RemoveTechnologyAsync(int id);
}
