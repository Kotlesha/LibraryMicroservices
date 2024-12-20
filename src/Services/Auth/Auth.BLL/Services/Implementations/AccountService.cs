using Auth.BLL.DTOs.RequestDTOs;
using Auth.BLL.DTOs.ResponseDTOs;
using Auth.BLL.Errors;
using Auth.BLL.Providers.Interfaces;
using Auth.BLL.Services.Interfaces;
using Auth.DAL.Models;
using Auth.DAL.Repositories.Implementations;
using Auth.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.Components.Jwt;
using Shared.Components.Results;

namespace Auth.BLL.Services.Implementations;

public class AccountService(
    IAccountRepository accountRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordHasherProvider passwordsProvider,
    ITokenProvider tokenProvider,
    IUserIdProvider userIdProvider,
    IOptions<JwtOptions> options,
    IUnitOfWork unitOfWork) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IPasswordHasherProvider _passwordProvider = passwordsProvider;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IUserIdProvider _userIdProvider = userIdProvider;
    private readonly JwtOptions jwtOptions = options.Value;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AuthDTO>> Login(LoginDTO loginDTO)
    {
        var account = await _accountRepository.GetAccountByEmailAsync(loginDTO.Email);

        if (account is null)
        {
            return Result.Failure<AuthDTO>(UserErrors.InvalidPasswordOrEmail);
        }

        var isCorrectPassword = _passwordProvider.VerifyPasswords(
            loginDTO.Password, account.HashPassword);

        if (!isCorrectPassword)
        {
            return Result.Failure<AuthDTO>(UserErrors.InvalidPasswordOrEmail);
        }

        var token = _tokenProvider.GenerateToken(account.Id);

        var refreshToken = new RefreshToken {
            Id = Guid.NewGuid(),
            Token = _tokenProvider.GenerateRefreshToken(),
            ExpiresOnUtc = DateTime.UtcNow.AddDays(jwtOptions.RefreshTokenExpirationInDays),
            AccountId = account.Id
        };

        _refreshTokenRepository.AddRefreshToken(refreshToken);
        await _unitOfWork.SaveChangesAsync();

        return new AuthDTO(token, refreshToken.Token);
    }

    public async Task<Result> Register(RegisterDTO registerDTO)
    {
        var hashPassword = _passwordProvider.GetPasswordHash(registerDTO.Password);

        var account = new Account
        {
            Id = Guid.NewGuid(),
            Email = registerDTO.Email,
            HashPassword = hashPassword
        };

        _accountRepository.AddAccount(account);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<AccountDTO>> GetAccountProfile()
    {
        var userId = Guid.Parse(_userIdProvider.GetAuthUserId());
        var account = await _accountRepository.GetAccountByIdAsync(userId);

        if (account is null)
        {
            return Result.Failure<AccountDTO>(UserErrors.NotFound);
        }

        var accountDTO = new AccountDTO(userId, account.Email);
        return accountDTO;
    }
}
