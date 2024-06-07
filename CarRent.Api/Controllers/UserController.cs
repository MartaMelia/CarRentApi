namespace CarRent.Api.Controllers;

[Route("api/user")]
public class UserController : ApiController
{
    public UserController(ISender mediator) : base(mediator) {}

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand request) 
    {
        return await Handle<RegisterUserCommand, DefaultResponse>(request);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        return await Handle<LoginCommand, AuthorizationResponse>(request);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        return await Handle<LogoutCommand, DefaultResponse>(new LogoutCommand(UserId()));
    }
}
