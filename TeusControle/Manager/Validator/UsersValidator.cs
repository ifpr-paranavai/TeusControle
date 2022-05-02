using Core.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Validator
{
    public class UsersValidator : AbstractValidator<Users>
    {
        public UsersValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(150);
        }
    }
}
