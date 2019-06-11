using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCms.MultiTenancy.Dto;

namespace MyCms.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

