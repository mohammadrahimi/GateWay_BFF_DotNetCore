using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace BFF.Web.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(params Permission[] permissiones)
    {
        var permission = string.Join(",", permissiones.ToArray());
        Policy = permission.ToString();
    }
}
