using System.Threading.Tasks;
using MyCms.Configuration.Dto;

namespace MyCms.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
