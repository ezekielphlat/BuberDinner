using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication.Common;

namespace BuberDinner.WebApplication.Services.Authentication;

public interface IAuthenticationQueryService
{
    AuthenticationResult Login(string email, string password);
}
  