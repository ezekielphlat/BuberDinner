using BuberDinner.Api.Filters;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contract.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.AddControllers;

[ApiController]
[Route("auth")]
//[ErrorHandlingFilter]  we can also add this to program cs so that it will apply to all controllers
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async  Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        var result = await _mediator.Send(command);
        var response = new AuthenticationResponse(result.user.Id, result.user.FirstName, result.user.LastName, result.user.Email, result.Token);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task< IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var result = await _mediator.Send(query);
        var response = new AuthenticationResponse(result.user.Id, result.user.FirstName, result.user.LastName, result.user.Email, result.Token);
        return Ok(response);
    }
}