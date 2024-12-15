using Auth.BLL.DTOs;
using Shared.Components.Results;

namespace Auth.BLL.Services.Interfaces;

public interface IAccountService
{
    Task<Result<string>> Login(LoginDTO loginDTO);
    Task<Result> Register(RegisterDTO registerDTO);
}
