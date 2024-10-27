using Microsoft.EntityFrameworkCore;
using TechStackStudies.DTOs;
using TechStackStudies.Infrastructure.Data;
using TechStackStudies.Infrastructure.Mappers;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;

namespace TechStackStudies.Infrastructure.Repositories;

public class TechnologyRepository : ITechnologyRepository
{
    private readonly AppDbContext _dbContext;

    public TechnologyRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<TechnologyResponse>> AddTechnologyAsync(TechnologyRequest newTechnology)
    {
        ServiceResponse<TechnologyResponse> serviceResponse = new ServiceResponse<TechnologyResponse>();
        TechnologyMapper technologyMapper = new TechnologyMapper();

        try
        {
            Technology technology = technologyMapper.TechnologyRequestToTechnology(newTechnology);

            await _dbContext.Technologies.AddAsync(technology);
            await _dbContext.SaveChangesAsync();

            TechnologyResponse technologyResponse = technologyMapper.TechnologyToTechnologyResponse(technology);

            serviceResponse.Data = technologyResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<TechnologyResponse>>> GetAllTechnologiesAsync()
    {
        ServiceResponse<IEnumerable<TechnologyResponse>> serviceResponse = new ServiceResponse<IEnumerable<TechnologyResponse>>();
        TechnologyMapper technologyMapper = new TechnologyMapper();

        try
        {
            IEnumerable<Technology> technologies = await _dbContext.Technologies
                .AsNoTracking()
                .ToListAsync();

            IEnumerable<TechnologyResponse> technologyResponse = technologies.Select(technologyMapper.TechnologyToTechnologyResponse);

            serviceResponse.Data = technologyResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<TechnologyResponse>> GetTechnologyByIdAsync(int id)
    {
        ServiceResponse<TechnologyResponse> serviceResponse = new ServiceResponse<TechnologyResponse>();
        TechnologyMapper technologyMapper = new TechnologyMapper();

        try
        {
            Technology techonlogy = await _dbContext.Technologies
                .FindAsync(id)
                ?? throw new Exception($"Technology witth Id \"{id}\" not found!");

            serviceResponse.Data = technologyMapper.TechnologyToTechnologyResponse(techonlogy);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> RemoveTechnologyAsync(int id)
    {
        ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

        try
        {
            Technology technology = await _dbContext.Technologies
                .FindAsync(id)
                ?? throw new Exception($"Technology witth Id \"{id}\" not found!");

            _dbContext.Remove(technology);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<TechnologyResponse>> UpdateTechnologyAsync(int id, TechnologyRequest updatedTechnology)
    {
        ServiceResponse<TechnologyResponse> serviceResponse = new ServiceResponse<TechnologyResponse>();
        TechnologyMapper technologyMapper = new TechnologyMapper();

        try
        {
            Technology technology = await _dbContext.Technologies
                .FindAsync(id)
                ?? throw new Exception($"Technology witth Id \"{id}\" not found!");

            technologyMapper.ApplyUpdate(updatedTechnology, technology);

            serviceResponse.Data = technologyMapper.TechnologyToTechnologyResponse(technology);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}
