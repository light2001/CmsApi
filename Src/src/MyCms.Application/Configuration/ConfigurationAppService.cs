﻿using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MyCms.Configuration.Dto;

namespace MyCms.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MyCmsAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
