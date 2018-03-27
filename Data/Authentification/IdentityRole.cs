using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Data.Authentification
{
    public class MyIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}