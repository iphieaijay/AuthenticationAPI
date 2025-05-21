using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Validators
{
   public class LoginUserRequestValidator: AbstractValidator<LoginRequest>
    {
            public LoginUserRequestValidator()
            {
                RuleFor(x => x.Email)
                  .NotEmpty().WithMessage("Email is required")
                  .EmailAddress().WithMessage("A valid email address is required");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required")
                    .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
            }
        }
    }
