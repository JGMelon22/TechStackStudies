using FluentAssertions;
using Moq;
using TechStackStudies.Application.Commands;
using TechStackStudies.Application.Handlers;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;
using TechStackStudies.Models.Enums;

namespace TechStackStudies.Tests.Application.Commands;

public class AddTechnologyCommandHandlerTests
{
    [Fact]
    public async Task Should_ReturnIncludeTechnology_When_AllPropertiesAreValid()
    {
        // Arrange
        Mock<ITechnologyRepository> technologyRepository = new Mock<ITechnologyRepository>();
        TechnologyRequest newTechnology = new TechnologyRequest("PHP", false, 8.3F, Category.Backend, SkillLevel.Beginner);
        AddTechnologyCommand command = new AddTechnologyCommand(newTechnology);
        AddTechnologyCommandHandler handler = new AddTechnologyCommandHandler(technologyRepository.Object);
        TechnologyResponse includedTechnolgy = new TechnologyResponse
        {
            Id = 1,
            Name = "PHP",
            IsFrameworkOrLib = false,
            CurrentVersion = 8.3F,
            Category = Category.Backend,
            SkillLevel = SkillLevel.Beginner
        };
        ServiceResponse<bool> serviceResponse = new()
        {
            Data = true,
            Success = true,
            Message = string.Empty
        };

        technologyRepository
            .Setup(x => x.AddTechnologyAsync(It.IsAny<Technology>()))
            .ReturnsAsync(serviceResponse);

        // Act
        ServiceResponse<bool> result = await handler.Handle(command);

        // Assert
        result.Success.Should().BeTrue();
        result.Message.Should().Be(string.Empty);

        technologyRepository.Verify(x => x.AddTechnologyAsync(It.IsAny<Technology>()), Times.Once);
    }
}
