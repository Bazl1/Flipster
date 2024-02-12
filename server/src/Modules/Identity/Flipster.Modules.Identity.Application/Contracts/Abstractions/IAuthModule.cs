using Flipster.Modules.Identity.Application.Dtos;
using Flipster.Modules.Identity.Application.Dtos.SignIn;
using Flipster.Modules.Identity.Application.Dtos.SignUp;
using Flipster.Modules.Identity.Domain.Common;

namespace Flipster.Modules.Identity.Application.Contracts.Abstractions;

public interface IAuthModule
{
    IdentityResult<AuthDto> SignUp(SignUpInputDto input);
    IdentityResult<AuthDto> SignIn(SignInInputDto input);
    IdentityResult SignOut();
    IdentityResult<AuthDto> Refresh();
}