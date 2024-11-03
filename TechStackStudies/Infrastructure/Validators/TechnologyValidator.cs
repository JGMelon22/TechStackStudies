using FluentValidation;
using TechStackStudies.DTOs;
using TechStackStudies.Models.Enums;

namespace TechStackStudies.Infrastructure.Validators;

public class TechnologyValidator : AbstractValidator<TechnologyRequest>
{
    public TechnologyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Technology Name can not be empty!")
            .NotNull()
            .WithMessage("Technology Name can not be null!")
            .MinimumLength(2)
            .WithMessage("Technology Name must contain at least 2 characters!")
            .MaximumLength(100)
            .WithMessage("Technology Name can not exceed at least 100 characters!");

        RuleFor(x => x.IsFrameworkOrLib)
            .NotNull()
            .WithMessage("Technology Is Framework or Lib can not be null!")
            .Must(isFrameworkOrLib => isFrameworkOrLib == false || isFrameworkOrLib == true);

        RuleFor(x => x.CurrentVersion)
            .NotEmpty()
            .WithName("Technology Current Version can not be empty!")
            .NotNull()
            .WithName("Technology Current Version can not be null!")
            .Must(currentVersion => currentVersion >= 0.0)
            .WithMessage("Technology Current Version must be greater than 0!");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Technology Category can not be empty!")
            .NotNull()
            .WithMessage("Technology Category can not be null!")
            .Must(category => Enum.IsDefined(typeof(Category), category))
            .WithMessage("A valid Category must be selected!");

        RuleFor(x => x.SkillLevel)
            .NotEmpty()
            .WithMessage("Technology Skill Level can not be empty!")
            .NotNull()
            .WithMessage("Technology Skill Level can not be null!")
            .Must(skillLevel => Enum.IsDefined(typeof(SkillLevel), skillLevel))
            .WithMessage("A valid Skill Level must be selected!");
    }
}
