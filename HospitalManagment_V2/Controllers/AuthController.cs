using HospitalManagment_V2.Mediator.Auth.SignIn;
using HospitalManagment_V2.Mediator.Auth.SignUp;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagment_V2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController(IMediator mediator) : ControllerBase
	{
		[HttpPost("/sign-in")]
		public async Task<IActionResult> SignIn([FromBody] SignInRequestDto request)
		{
			return Ok(await mediator.Send(new SignInCommand(request)));
		}

		[HttpPost("/sign-up")]
		public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto request)
		{
			return Ok(await mediator.Send(new SignUpCommand(request)));
		}
	}
}
