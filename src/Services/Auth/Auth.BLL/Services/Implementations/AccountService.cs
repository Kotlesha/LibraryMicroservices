using Auth.BLL.DTOs.RequestDTOs;
using Auth.BLL.DTOs.ResponseDTOs;
using Auth.BLL.Errors;
using Auth.BLL.Providers.Interfaces;
using Auth.BLL.Services.Interfaces;
using Auth.DAL.Models;
using Auth.DAL.Repositories.Implementations;
using Auth.DAL.Repositories.Interfaces;
using FluentValidation;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.Components.Errors;
using Shared.Components.Results;

namespace Auth.BLL.Services.Implementations;

public class AccountService(
    IAccountRepository accountRepository,
    IPasswordHasherProvider passwordsProvider,
    IValidator<LoginDTO> loginDTOValidator,
    IValidator<RegisterDTO> registerDTOValidator,
    ITokenProvider tokenProvider,
    IUserIdProvider userIdProvider,
    IUnitOfWork unitOfWork) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IPasswordHasherProvider _passwordProvider = passwordsProvider;
    private readonly IValidator<LoginDTO> _loginDTOValidator = loginDTOValidator;
    private readonly IValidator<RegisterDTO> _registerDTOValidator = registerDTOValidator;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IUserIdProvider _userIdProvider = userIdProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Login(LoginDTO loginDTO)
    {
        await _loginDTOValidator.ValidateAndThrowAsync(loginDTO);

        var account = await _accountRepository.GetAccountByEmailAsync(loginDTO.Email);

        if (account is null)
        {
            return Result.Failure<string>(UserErrors.InvalidPasswordOrEmail);
        }

        var isCorrectPassword = _passwordProvider.VerifyPasswords(
            loginDTO.Password, account.HashPassword);

        if (!isCorrectPassword)
        {
            return Result.Failure<string>(UserErrors.InvalidPasswordOrEmail);
        }

        var token = _tokenProvider.GenerateToken(account.Id);
        return token;
    }

    public async Task<Result> Register(RegisterDTO registerDTO)
    {
        await _registerDTOValidator.ValidateAndThrowAsync(registerDTO);

        var passwordHash = _passwordProvider.GetPasswordHash(registerDTO.Password);

        var account = new Account(
            Guid.NewGuid(),
            registerDTO.Email,
            passwordHash);

        _accountRepository.AddAccount(account);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<AccountDTO>> GetAccountProfile()
    {
        var userId = _userIdProvider.GetAuthUserId();

        if (!Guid.TryParse(userId, out var Id))
        {
            return Result.Failure<AccountDTO>(Error.Failure);
        }

        var account = await _accountRepository.GetAccountByIdAsync(Id);

        if (account is null)
        {
            return Result.Failure<AccountDTO>(UserErrors.NotFound);
        }

        var accountDTO = new AccountDTO(account.Email);
        return accountDTO;
    }
}
