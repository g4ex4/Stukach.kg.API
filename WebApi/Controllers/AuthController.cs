namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator, AuthManager authManager)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<Response> Register(RegisterUserRequest request)
        {
            var response = await _mediator.Send(request);
            return response;
        }
        [HttpPost("login")]
        public async Task<AuthResponse> Login(LoginUserRequest request)
        {
            var response = await _mediator.Send(request);
            return response;
        }
        
    }
}
