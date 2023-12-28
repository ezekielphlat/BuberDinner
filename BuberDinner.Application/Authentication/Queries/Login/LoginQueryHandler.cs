
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>

{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;

    }

    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancel)
    {

        // 1. validate the user exists
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            throw new Exception("User with given email already exists");
        }
        // 2. validate the password
        if (user.Password != query.Password)
        {
            throw new Exception("Invalid password.");
        }
        // 3. create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}

