using DeployService.Api.Contacts.Auth;
using FastEndpoints;
using FluentValidation;

namespace DeployService.Api.Validators.Auth;

public sealed class RegisterRequestValidator
    : Validator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(254);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(12)
            .MaximumLength(128);

        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(80);
    }
}