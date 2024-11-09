using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechStackStudies.Application.Commands;
using TechStackStudies.Controllers;
using TechStackStudies.DTOs;
using TechStackStudies.Models;
using TechStackStudies.Models.Enums;
using Wolverine;

namespace TechStackStudies.Tests.Controllers;

public class TechnologiesControllerTests
{
    [Fact]
    public async Task Should_ReturnSuccess_When_NewTechnologyDetailsAreValid()
    {
        // Arrange
        Mock<IValidator<TechnologyRequest>> validator = new Mock<IValidator<TechnologyRequest>>();
        Mock<IMessageBus> messageBus = new Mock<IMessageBus>();
        ValidationResult validationResult = new ValidationResult();
        TechnologiesController controller = new TechnologiesController(validator.Object, messageBus.Object);
        TechnologyRequest newTechnology = new TechnologyRequest(
            "ASP .NET Core",
            true, 9.0F,
            Category.Backend,
            SkillLevel.Skilled
            );

        TechnologyResponse technologyResponse = new TechnologyResponse
        {
            Id = 1,
            Name = "ASP .NET Core",
            IsFrameworkOrLib = true,
            CurrentVersion = 9.0F,
            Category = Category.Backend,
            SkillLevel = SkillLevel.Skilled
        };

        ServiceResponse<TechnologyResponse> serviceResponse = new ServiceResponse<TechnologyResponse>
        {
            Data = technologyResponse,
            Success = true,
            Message = string.Empty
        };

        validator
            .Setup(x => x.ValidateAsync(newTechnology, default))
            .ReturnsAsync(validationResult);

        messageBus
            .Setup(x => x.InvokeAsync<ServiceResponse<TechnologyResponse>>(It.Is<AddTechnologyCommand>(cmd => cmd.NewTechnology == newTechnology), default, null))
            .ReturnsAsync(serviceResponse);

        // Act
        IActionResult result = await controller.AddNewTechnologyAsync(newTechnology);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        serviceResponse.Data.Should().Be(technologyResponse);
    }
}
