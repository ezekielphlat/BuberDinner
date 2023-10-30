using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using BuberDinner.WebApplication.Services.Authentication;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public AuthenticationResult Login(string email, string password)
    {
        // 1. validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("user with given email already exists. ");
        }
        // 2. validate the password
        if (user.Password != password)
        {
            throw new Exception("Invalid password.");
        }
        // 3. create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // check if user already exists
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("user with given email already exists. ");
        }
        // create user (generate unique ID) persist the user to db
        var user = new User { FirstName = firstName, LastName = lastName, Email = email, Password = password };
        _userRepository.Add(user);
        // create jwt token

        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }
}
