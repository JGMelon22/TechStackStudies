using TechStackStudies.Models;

namespace TechStackStudies.Interfaces;

public interface ITechnologyRepository
{
    Task<ServiceResponse<PagedResponseOffset<Technology>>> GetAllTechnologiesAsync(int pageNumber = 1, int pageSize = 10);
    Task<ServiceResponse<Technology>> GetTechnologyByIdAsync(int id);
    Task<ServiceResponse<bool>> AddTechnologyAsync(Technology newTechnology);
    Task<ServiceResponse<bool>> UpdateTechnologyAsync(int id, Technology updatedTechnology);
    Task<ServiceResponse<bool>> RemoveTechnologyAsync(int id);
}
