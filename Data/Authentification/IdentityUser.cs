using System;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Data.Authentification
{
    public class MyIdentityUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}