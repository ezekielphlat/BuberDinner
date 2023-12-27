using BuberDinner.Api.Filters;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Contract.Authentication;
using BuberDinner.WebApplication.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.AddControllers;

[ApiController]
[Route("auth")]
//[ErrorHandlingFilter]  we can also add this to program cs so that it will apply to all controllers
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationCommandService _authenticationCommandService;
    private readonly IAuthenticationQueryService _authenticationQueryService;

    public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationCommandService = authenticationCommandService;
        _authenticationQueryService = authenticationQueryService;
    }
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = _authenticationCommandService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        var response = new AuthenticationResponse(result.user.Id, result.user.FirstName, result.user.LastName, result.user.Email, result.Token);
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = _authenticationQueryService.Login(request.Email, request.Password);
        var response = new AuthenticationResponse(result.user.Id, result.user.FirstName, result.user.LastName, result.user.Email, result.Token);
        return Ok(response);
    }
}