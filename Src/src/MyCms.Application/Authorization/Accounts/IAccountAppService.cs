using System.Threading.Tasks;
using Abp.Application.Services;
using MyCms.Authorization.Accounts.Dto;

namespace MyCms.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
