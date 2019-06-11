using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyCms.Configuration;
using MyCms.Web;

namespace MyCms.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MyCmsDbContextFactory : IDesignTimeDbContextFactory<MyCmsDbContext>
    {
        public MyCmsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MyCmsDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MyCmsDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MyCmsConsts.ConnectionStringName));

            return new MyCmsDbContext(builder.Options);
        }
    }
}
