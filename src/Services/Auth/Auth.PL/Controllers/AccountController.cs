using Auth.BLL.DTOs;
using Auth.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Extensions;

namespace Auth.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;

    [HttpPost("/login")]
    public async Task<IResult> Login(LoginDTO loginDTO)
    {
        var result = await _accountService.Login(loginDTO);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return TypedResults.Ok(result.Value);
    }

    [HttpPost("/register")]
    public async Task<IResult> Register(RegisterDTO registerDTO)
    {
        var result = await _accountService.Register(registerDTO);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok();
    }

    //[Authorize]
    //[HttpGet("/profile")]
    //public async Task<IResult> GetUserProfile()
    //{
    //    var user = 
    //}
}
