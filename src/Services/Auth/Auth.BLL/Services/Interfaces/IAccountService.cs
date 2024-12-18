using Auth.BLL.DTOs.RequestDTOs;
using Auth.BLL.DTOs.ResponseDTOs;
using Shared.Components.Results;

namespace Auth.BLL.Services.Interfaces;

public interface IAccountService
{
    Task<Result<AuthDTO>> Login(LoginDTO loginDTO);
    Task<Result> Register(RegisterDTO registerDTO);
    Task<Result<AccountDTO>> GetAccountProfile();
}
