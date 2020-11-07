using System;
using FluentValidation;
using Shb.Application.DTOs;

namespace Shb.Application.Validators
{
    public class SuperheroDTOValidator : AbstractValidator<SuperheroDTO>
    {
        public SuperheroDTOValidator()
        {
            RuleFor(sh => sh.Name)
                .Length(10, 50).WithMessage("{PropertyName} value length should be between 10 and 50!");
            
            RuleFor(sh => sh.Attack)
                .LessThanOrEqualTo(10).WithMessage("{PropertyName} value should be less than or equal to 10!");

            RuleFor(sh => sh.Defence)
                .LessThanOrEqualTo(10).WithMessage("{PropertyName} value should be less than or equal to 10!");
        }
    }
}
