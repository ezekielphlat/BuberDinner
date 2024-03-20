

using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using FluentValidation;
using MediatR;

namespace BuberDinner.Application.Common.Behaviors;

public class ValidateRegisterCommandBehavior : IPipelineBehavior<RegisterCommand, AuthenticationResult>
{
    private readonly IValidator<RegisterCommand> _validator;

    public ValidateRegisterCommandBehavior(IValidator<RegisterCommand> validator)
    {
        _validator = validator;
    }
    public async Task<AuthenticationResult> Handle(RegisterCommand request, RequestHandlerDelegate<AuthenticationResult> next, CancellationToken cancellationToken)
    {
        // before the handler
        var validationResult = await _validator.ValidateAsync(request);
        if (validationResult.IsValid) { return await next(); }
        var errors = validationResult.Errors;

        // var result = await next();
        // after the handler
        throw new InvalidOperationException();
    }
}

