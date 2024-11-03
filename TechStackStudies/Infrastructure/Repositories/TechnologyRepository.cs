using System.Reflection;
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
    private readonly ILogger<TechnologyRepository> _logger;

    public TechnologyRepository(AppDbContext dbContext, ILogger<TechnologyRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
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
            string methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            IEnumerable<Technology> technologies = await _dbContext.Technologies
                .AsNoTracking()
                .ToListAsync();

            _logger.LogInformation("{MethodName} {ObjectName}: {@Technologies}", methodNameLog, nameof(technologies), technologies);

            IEnumerable<TechnologyResponse> technologyResponse = technologies.Select(technologyMapper.TechnologyToTechnologyResponse);

            serviceResponse.Data = technologyResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {MethodName}: {Message}", $"{GetType().Name} -> {MethodBase.GetCurrentMethod()?.Name}", ex.Message);

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
            string methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            Technology technology = await _dbContext.Technologies
                .FindAsync(id)
                ?? throw new Exception($"Technology with Id \"{id}\" not found!");

            _logger.LogInformation("{MethodName} {ObjectName}: {@Technology}", methodNameLog, nameof(technology), technology);

            serviceResponse.Data = technologyMapper.TechnologyToTechnologyResponse(technology);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {MethodName}: {Message}", $"{GetType().Name} -> {MethodBase.GetCurrentMethod()?.Name}", ex.Message);

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
            string methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            Technology technology = await _dbContext.Technologies
                .FindAsync(id)
                ?? throw new Exception($"Technology with Id \"{id}\" not found!");

            _logger.LogInformation("{MethodName} {ObjectName}: {@Technology}", methodNameLog, nameof(technology), technology);

            _dbContext.Remove(technology);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {MethodName}: {Message}", $"{GetType().Name} -> {MethodBase.GetCurrentMethod()?.Name}", ex.Message);

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
            string methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            Technology technology = await _dbContext.Technologies
                .FindAsync(id)
                ?? throw new Exception($"Technology with Id \"{id}\" not found!");

            _logger.LogInformation("{MethodName} {ObjectName}: {@Technology}", methodNameLog, nameof(technology), technology);

            technologyMapper.ApplyUpdate(updatedTechnology, technology);
            await _dbContext.SaveChangesAsync();

            serviceResponse.Data = technologyMapper.TechnologyToTechnologyResponse(technology);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {MethodName}: {Message}", $"{GetType().Name} -> {MethodBase.GetCurrentMethod()?.Name}", ex.Message);

            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}
