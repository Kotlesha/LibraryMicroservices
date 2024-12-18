using Auth.BLL.DTOs.RequestDTOs;
using Auth.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }


    [HttpPost("/register")]
    public async Task<IResult> Register(RegisterDTO registerDTO)
    {
        var result = await _accountService.Register(registerDTO);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }

    [Authorize]
    [HttpGet("/profile")]
    public async Task<IResult> GetAccountProfile()
    {
        var result = await _accountService.GetAccountProfile();

        return result.IsSuccess ? 
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }
}
