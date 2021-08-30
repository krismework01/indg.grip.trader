using FluentValidation;

namespace INDG.GRIP.Trader.Application.Logic.Auth
{
    public class SignInCommandValidation : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidation()
        {
            RuleFor(x => x.Login)
                .NotNull()
                .NotEmpty()
                .WithMessage("Login is required");
            
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}