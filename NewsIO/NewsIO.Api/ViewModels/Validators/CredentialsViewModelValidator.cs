using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.ViewModels.Validators
{
    public class CredentialsViewModelValidator : AbstractValidator<CredentialsViewModel>
    {
        public CredentialsViewModelValidator()
        {
            RuleFor(vm => vm.UserName).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(vm => vm.Password).MinimumLength(8).WithMessage("Password must be between 8 characters minimum in length");
        }
    }
}
