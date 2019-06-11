using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MyCms.Authorization.Roles;
using MyCms.Authorization.Users;
using MyCms.MultiTenancy;

namespace MyCms.EntityFrameworkCore
{
    public class MyCmsDbContext : AbpZeroDbContext<Tenant, Role, User, MyCmsDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public MyCmsDbContext(DbContextOptions<MyCmsDbContext> options)
            : base(options)
        {
        }
    }
}
