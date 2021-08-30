using FluentValidation;

namespace INDG.GRIP.Trader.Application.Logic.Users.Create
{
    public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidation()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login is required");
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName is required");
        }
    }
}