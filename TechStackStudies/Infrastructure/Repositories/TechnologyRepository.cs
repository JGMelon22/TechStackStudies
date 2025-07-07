using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TechStackStudies.Infrastructure.Data;
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

    public async Task<ServiceResponse<bool>> AddTechnologyAsync(Technology newTechnology)
    {
        ServiceResponse<bool> serviceResponse = new();

        try
        {
            await _dbContext.Technologies.AddAsync(newTechnology);
            await _dbContext.SaveChangesAsync();

            serviceResponse.Data = true;

        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<PagedResponseOffset<Technology>>> GetAllTechnologiesAsync(int pageNumber = 1, int pageSize = 10)
    {
        ServiceResponse<PagedResponseOffset<Technology>> serviceResponse = new();

        try
        {
            string methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            int skip = (pageNumber - 1) * pageSize;

            int totalRecords = await _dbContext.Technologies
                                                .AsNoTracking()
                                                .CountAsync();

            List<Technology> technologies = await _dbContext.Technologies
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync() ?? [];

            PagedResponseOffset<Technology> result = new(technologies, pageNumber, pageSize, totalRecords);

            _logger.LogInformation("{MethodName} {ObjectName}: {@Technologies}", methodNameLog, nameof(technologies), technologies);

            serviceResponse.Data = result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {MethodName}: {Message}", $"{GetType().Name} -> {MethodBase.GetCurrentMethod()?.Name}", ex.Message);

            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<Technology>> GetTechnologyByIdAsync(int id)
    {
        ServiceResponse<Technology> serviceResponse = new();

        try
        {
            string methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            Technology technology = await _dbContext.Technologies
                .FindAsync(id)
                ?? throw new Exception($"Technology with Id \"{id}\" not found!");

            _logger.LogInformation("{MethodName} {ObjectName}: {@Technology}", methodNameLog, nameof(technology), technology);

            serviceResponse.Data = technology;
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
        ServiceResponse<bool> serviceResponse = new();

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

    public async Task<ServiceResponse<bool>> UpdateTechnologyAsync(int id, Technology updatedTechnology)
    {
        ServiceResponse<bool> serviceResponse = new();

        try
        {
            string methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            Technology technology = await _dbContext.Technologies
                .FindAsync(id)
                ?? throw new Exception($"Technology with Id \"{id}\" not found!");

            technology.Name = updatedTechnology.Name;
            technology.IsFrameworkOrLib = updatedTechnology.IsFrameworkOrLib;
            technology.CurrentVersion = updatedTechnology.CurrentVersion;
            technology.Category = updatedTechnology.Category;
            technology.SkillLevel = updatedTechnology.SkillLevel;

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("{MethodName} {ObjectName}: {@Technology}", methodNameLog, nameof(technology), technology);

            serviceResponse.Data = true;
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
