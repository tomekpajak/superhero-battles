using System;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shb.Application.DTOs;
using Shb.Application.Services;
using Shb.Application.Services.Abstraction;
using Shb.Application.Validators;

namespace Shb.Application.Configurations
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services) 
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ISuperheroService, SuperheroService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidator<SuperheroDTO>, SuperheroDTOValidator>();

            return services;
        }
    }
}
