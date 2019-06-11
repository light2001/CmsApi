using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MyCms.EntityFrameworkCore
{
    public static class MyCmsDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MyCmsDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MyCmsDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
