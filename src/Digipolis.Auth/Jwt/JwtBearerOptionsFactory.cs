﻿using Digipolis.Auth.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Digipolis.Auth.Jwt
{
    internal class JwtBearerOptionsFactory
    {
        public static JwtBearerOptions Create(AuthOptions authOptions, IJwtSigningKeyResolver signingKeyResolver, ILogger<JwtBearerMiddleware> logger)
        {
            var jwtBearerOptions = new JwtBearerOptions
            {
                AutomaticAuthenticate = true
            };

            jwtBearerOptions.TokenValidationParameters = TokenValidationParametersFactory.Create(authOptions, signingKeyResolver);

            jwtBearerOptions.Events = new JwtBearerEvents()
            {
                //OnTokenValidated = context =>
                //{
                //    return Task.FromResult<object>(null);
                //},
                OnAuthenticationFailed = context =>
                {
                    logger.LogInformation($"Jwt token validation failed. Exception: {context.Exception.ToString()}");

                    context.Ticket = new Microsoft.AspNetCore.Authentication.AuthenticationTicket(new ClaimsPrincipal(), new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties(), string.Empty);
                    context.HandleResponse();

                    return Task.FromResult<object>(null);
                },
                //OnChallenge = context =>
                //{
                //    return Task.FromResult<object>(null);
                //},
                //OnMessageReceived = context =>
                //{
                //    //the signingKey is resolved on this event because we can make the call async here, in the signatureValidator async is not possible
                //    //if (!String.IsNullOrWhiteSpace(authOptions.JwtSigningKeyProviderUrl))
                //    //context.Options.TokenValidationParameters.IssuerSigningKey = await signingKeyProvider.ResolveSigningKeyAsync(true);
                //    return Task.FromResult<object>(null);
                //}
            };

            return jwtBearerOptions;
        }
    }
}
