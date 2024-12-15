using Auth.BLL.DTOs;
using Auth.BLL.Errors;
using Auth.BLL.Providers.Interfaces;
using Auth.BLL.Services.Interfaces;
using Auth.DAL.Models;
using Auth.DAL.Repositories.Implementations;
using Auth.DAL.Repositories.Interfaces;
using FluentValidation;
using Shared.Components.Results;

namespace Auth.BLL.Services.Implementations;

public class AccountService(
    IAccountRepository accountRepository,
    IPasswordHasherProvider passwordsProvider,
    IValidator<LoginDTO> loginDTOValidator,
    IValidator<RegisterDTO> registerDTOValidator,
    ITokenProvider tokenProvider,
    IUnitOfWork unitOfWork) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IPasswordHasherProvider _passwordProvider = passwordsProvider;
    private readonly IValidator<LoginDTO> _loginDTOValidator = loginDTOValidator;
    private readonly IValidator<RegisterDTO> _registerDTOValidator = registerDTOValidator;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Login(LoginDTO loginDTO)
    {
        await _loginDTOValidator.ValidateAndThrowAsync(loginDTO);

        var account = await _accountRepository.GetAccountByEmail(loginDTO.Email);

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

        await _accountRepository.AddAccountAsync(account);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
