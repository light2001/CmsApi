using Abp.Authorization;
using MyCms.Authorization.Roles;
using MyCms.Authorization.Users;

namespace MyCms.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
