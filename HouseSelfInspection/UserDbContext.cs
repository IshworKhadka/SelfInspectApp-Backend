using HouseSelfInspection.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseSelfInspection
{
    public class UserDbContext: IdentityDbContext<ApplicationUserModel>
    {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUserModel> ApplicationUsers { get; set; }


    }
}
