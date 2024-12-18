﻿using Auth.BLL.Providers.Implementations;
using Auth.BLL.Providers.Interfaces;
using Auth.BLL.Services.Implementations;
using Auth.BLL.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Auth.BLL.Extensions;

public static class BusinessLogicLayerExtensions
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddSingleton<IPasswordHasherProvider, PasswordHasherProvider>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.LanguageManager.Enabled = false;

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}
