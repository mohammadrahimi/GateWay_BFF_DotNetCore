using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace BFF.Web.Authentication;

public class PermissionAuthorizationHandler
     : AuthorizationHandler<PermissionRequirement>
{
    private readonly JwtSettings _jwtSettings;

    public PermissionAuthorizationHandler(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.Identity!.IsAuthenticated)
        {
            var hasClaimRole = context.User.HasClaim(x => x.Type == "Roles");
            if (hasClaimRole)
            {
                var _Roles = context.User.Claims.Where(x => x.Type == "Roles")
                    .Select(c => c.Value)
                    .ToList();

                if (_Roles is not null)
                {
                    var _found = requirement.Permissiones.Where(
                                       x => _Roles.Contains(x.ToString()));
                    if (_found.Count() > 0)
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }

                }
            }
        }

        context.Fail();
        return Task.CompletedTask;
    }

}
