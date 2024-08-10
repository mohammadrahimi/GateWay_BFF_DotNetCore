using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace BFF.Web.Authentication;

public class PermissionAuthorizationPolicyProvider
    : DefaultAuthorizationPolicyProvider
{
    public PermissionAuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options)
        : base(options)
    {
    }
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);
        if (policy is not null)
        {
            return policy;
        }
        List<Permission> permissions = new();
        permissions = policyName.Split(",")
            .Select(x => (Permission)Enum.Parse(typeof(Permission), x))
            .ToList();

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(permissions))
            .Build();
    }
}
