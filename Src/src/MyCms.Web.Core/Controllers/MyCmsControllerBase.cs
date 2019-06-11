using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MyCms.Controllers
{
    public abstract class MyCmsControllerBase: AbpController
    {
        protected MyCmsControllerBase()
        {
            LocalizationSourceName = MyCmsConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
