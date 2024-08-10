using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace BFF.Web.Authentication;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(List<Permission> permissiones)
    {
        Permissiones = permissiones;
    }

    public List<Permission> Permissiones { get; }

}
