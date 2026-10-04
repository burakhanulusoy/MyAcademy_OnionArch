using FluentValidation;
using ProjectApp.Application.Features.Comments.ProductComments;

namespace ProjectApp.Application.Validators.ProductValidators
{
    public class UpdateProductValidator:AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Ürün adý boþ olamaz.")
               .MinimumLength(2).WithMessage("Ürün adý en az 2 karakter olmalýdýr.")
               .MaximumLength(100).WithMessage("Ürün adý en fazla 100 karakter olabilir.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Fiyat boþ olamaz.")
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalýdýr.")
                .LessThanOrEqualTo(1_000_000).WithMessage("Fiyat 1.000.000'dan büyük olamaz.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açýklama boþ olamaz.")
                .MaximumLength(500).WithMessage("Açýklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Kategori seçilmelidir.")
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir.");
        }
    }
}
