using LibraryManagement.Data.Authentification;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class MyIdentityDbContext: IdentityDbContext<MyIdentityUser,MyIdentityRole,string>
    {
        public MyIdentityDbContext (DbContextOptions<MyIdentityDbContext> options) : base(options)
        {
        }
        
    }
}