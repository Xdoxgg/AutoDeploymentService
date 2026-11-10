using DeployService.Api.Contacts.Auth;
using FastEndpoints;
using FluentValidation;

namespace DeployService.Api.Validators.Auth;


public sealed class LoginRequestValidator
    : Validator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(254);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(128);
    }
}