using FluentValidation;

namespace INDG.GRIP.Trader.Application.Logic.Products.Create
{
    public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greate that 0");
        }
    }
}
