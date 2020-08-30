using ITCraftTask.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITCraftTask.DataAccessLayer.AppContext
{
    public class ApplicationContext: IdentityDbContext<User, Role, long>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
