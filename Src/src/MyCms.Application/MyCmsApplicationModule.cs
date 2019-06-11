using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyCms.Authorization;

namespace MyCms
{
    [DependsOn(
        typeof(MyCmsCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MyCmsApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MyCmsAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MyCmsApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
