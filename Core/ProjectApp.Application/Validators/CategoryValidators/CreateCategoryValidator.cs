using FluentValidation;
using ProjectApp.Application.Features.Comments.CategoryComments;

namespace ProjectApp.Application.Validators.CategoryValidators
{
    public class CreateCategoryValidator:AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ýsim boþ olamaz.")
                .MaximumLength(100).WithMessage("Ýsim 100 karakteri geçemez.")
                .MinimumLength(3).WithMessage("Ýsim 3 karakterden az olamaz.");

        }
    }
}
