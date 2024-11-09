using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechStackStudies.Application.Commands;
using TechStackStudies.Application.Queries;
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

    [Fact]
    public async Task Should_ReturnSuccess_When_TechnologyWithProvidedIdIsFound()
    {
        // Arrange
        Mock<IValidator<TechnologyRequest>> validator = new Mock<IValidator<TechnologyRequest>>();
        Mock<IMessageBus> messageBus = new Mock<IMessageBus>();
        TechnologiesController controller = new TechnologiesController(validator.Object, messageBus.Object);
        TechnologyResponse technologyResponse = new TechnologyResponse
        {
            Id = 2,
            Name = "Vue.js",
            IsFrameworkOrLib = true,
            CurrentVersion = 3.2F,
            Category = Category.Frontend,
            SkillLevel = SkillLevel.Beginner
        };

        ServiceResponse<TechnologyResponse> serviceResponse = new ServiceResponse<TechnologyResponse>
        {
            Data = technologyResponse,
            Success = true,
            Message = string.Empty
        };

        messageBus
            .Setup(x => x.InvokeAsync<ServiceResponse<TechnologyResponse>>(It.Is<GetTechnologyByIdQuery>(qry => qry.Id == 2), default, null))
                .ReturnsAsync(serviceResponse);

        // Act
        IActionResult result = await controller.GetTechnologyByIdAsync(2);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        serviceResponse.Data.Should().Be(technologyResponse);
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenTechnologyCollectionIsNotEmpty()
    {
        // Arrange
        Mock<IValidator<TechnologyRequest>> validator = new Mock<IValidator<TechnologyRequest>>();
        Mock<IMessageBus> messageBus = new Mock<IMessageBus>();
        TechnologiesController controller = new TechnologiesController(validator.Object, messageBus.Object);
        IEnumerable<TechnologyResponse> technologies = new List<TechnologyResponse>
        {
            new ()
            {
                Id = 1,
                Name = ".NET",
                IsFrameworkOrLib = true,
                CurrentVersion = 9.0F,
                Category = Models.Enums.Category.Backend,
                SkillLevel = Models.Enums.SkillLevel.Skilled
            },
            new ()
            {
                Id = 2,
                Name = "SQL Server",
                IsFrameworkOrLib = false,
                CurrentVersion = 2022,
                Category = Models.Enums.Category.Database,
                SkillLevel = Models.Enums.SkillLevel.Skilled
            },
            new ()
            {
                Id = 3,
                Name = "Docker",
                IsFrameworkOrLib = false,
                CurrentVersion = 27.3F,
                Category = Category.Devops,
                SkillLevel = SkillLevel.Beginner
            }
        };

        ServiceResponse<IEnumerable<TechnologyResponse>> serviceResponse = new ServiceResponse<IEnumerable<TechnologyResponse>>
        {
            Data = technologies,
            Success = true,
            Message = string.Empty
        };

        // ServiceResponse<IEnumerable<TechnologyResponse>> technologies = await _messageBus.InvokeAsync<ServiceResponse<IEnumerable<TechnologyResponse>>>(new GetTechnologiesQuery());
        messageBus
            .Setup(x => x.InvokeAsync<ServiceResponse<IEnumerable<TechnologyResponse>>>(It.IsAny<GetTechnologiesQuery>(), default, null))
                .ReturnsAsync(serviceResponse);

        // Act
        IActionResult result = await controller.GetAllTechnologiesAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        serviceResponse.Data.Count().Should().Be(3);
    }

    [Fact]
    public async Task Should_ReturnSuccess_When_TechnologyToUpdateIsFoundAndValidInput()
    {
        // Arrange
        Mock<IValidator<TechnologyRequest>> validator = new Mock<IValidator<TechnologyRequest>>();
        Mock<IMessageBus> messageBus = new Mock<IMessageBus>();
        ValidationResult validationResult = new ValidationResult();
        TechnologiesController controller = new TechnologiesController(validator.Object, messageBus.Object);
        TechnologyRequest updatedTechnolgy = new TechnologyRequest(
            "Grafana",
            false,
            11.3F,
            Category.Devops,
            SkillLevel.Beginner
        );

        TechnologyResponse response = new TechnologyResponse
        {
            Id = 3,
            Name = "Grafana",
            IsFrameworkOrLib = false,
            CurrentVersion = 11.3F,
            Category = Category.Devops,
            SkillLevel = SkillLevel.Beginner
        };

        ServiceResponse<TechnologyResponse> serviceResponse = new ServiceResponse<TechnologyResponse>
        {
            Data = response,
            Success = true,
            Message = string.Empty
        };

        validator
            .Setup(x => x.ValidateAsync(updatedTechnolgy, default))
            .ReturnsAsync(validationResult);

        messageBus
            .Setup(x => x.InvokeAsync<ServiceResponse<TechnologyResponse>>(It.Is<UpdateTechnologyCommand>(cmd => cmd.Id == 3 && cmd.UpdatedTechnology == updatedTechnolgy), default, null))
            .ReturnsAsync(serviceResponse);

        // Act
        IActionResult result = await controller.UpdateTechnologyAsync(3, updatedTechnolgy);

        // Assett
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        serviceResponse.Data.Should().Be(response);
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenTechnologyIdIsFound()
    {
        // Arrange
        Mock<IValidator<TechnologyRequest>> validator = new Mock<IValidator<TechnologyRequest>>();
        Mock<IMessageBus> messageBus = new Mock<IMessageBus>();
        TechnologiesController controller = new TechnologiesController(validator.Object, messageBus.Object);

        ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>
        {
            Success = true,
            Message = string.Empty
        };

        messageBus
            .Setup(x => x.InvokeAsync<ServiceResponse<bool>>(It.Is<RemoveTechnologyCommand>(qry => qry.Id == 3), default, null))
            .ReturnsAsync(serviceResponse);
        // Act
        IActionResult result = await controller.RemoveTechnologyAsync(3);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }
}
