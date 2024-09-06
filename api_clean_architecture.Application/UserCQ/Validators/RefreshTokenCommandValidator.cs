using api_clean_architecture.Application.UserCQ.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_clean_architecture.Application.UserCQ.Validators
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("O campo username não pode estar vazio");
        }
    }
}
