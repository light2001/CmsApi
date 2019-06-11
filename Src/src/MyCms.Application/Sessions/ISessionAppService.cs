using System.Threading.Tasks;
using Abp.Application.Services;
using MyCms.Sessions.Dto;

namespace MyCms.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
