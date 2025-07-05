using FluentAssertions;
using Moq;
using TechStackStudies.Application.Handlers;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;
using TechStackStudies.Models.Enums;

namespace TechStackStudies.Application.Commands;

public class UpdateTechnologyCommandHandlerTests
{
    [Fact]
    public async Task Should_ReturnUpdatedTechnology_When_AllPropertiesAreValid()
    {
        // Arrange
        Mock<ITechnologyRepository> technologyRepository = new Mock<ITechnologyRepository>();
        TechnologyResponse includedTechnolgy = new TechnologyResponse
        {
            Id = 1,
            Name = "MongoDB",
            IsFrameworkOrLib = false,
            CurrentVersion = 1.0F,
            Category = Category.Frontend,
            SkillLevel = SkillLevel.Beginner
        };
        TechnologyRequest updatedTechnology = new TechnologyRequest("MongoDB", false, 8.0F, Category.Database, SkillLevel.Beginner);
        UpdateTechnologyCommand command = new UpdateTechnologyCommand(1, updatedTechnology);
        UpdateTechnologyCommandHandler handler = new UpdateTechnologyCommandHandler(technologyRepository.Object);
        TechnologyResponse postUpdatedResponse = includedTechnolgy with
        {
            Id = 1,
            Name = updatedTechnology.Name,
            IsFrameworkOrLib = updatedTechnology.IsFrameworkOrLib,
            CurrentVersion = updatedTechnology.CurrentVersion,
            Category = updatedTechnology.Category,
            SkillLevel = updatedTechnology.SkillLevel
        };
        ServiceResponse<bool> serviceResponse = new()
        {
            Data = true,
            Success = true,
            Message = string.Empty
        };

        technologyRepository
            .Setup(x => x.UpdateTechnologyAsync(1, It.IsAny<Technology>()))
            .ReturnsAsync(serviceResponse);

        // Act
        ServiceResponse<bool> result = await handler.Handle(command);

        // Assert
        result.Success.Should().BeTrue();
        result.Message.Should().Be(string.Empty);

        technologyRepository.Verify(x => x.UpdateTechnologyAsync(1, It.IsAny<Technology>()), Times.Once);
    }
}
