using HouseSelfInspection.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
           
        }

        public DbSet<HouseModel> Houses { get; set; }
        public DbSet<TenantModel> Tenants { get; set; }
        public DbSet<LoginModel> Login { get; set; }

        public DbSet<InspectionScheduleModel> InspectionSchedules { get; set; }

    }
}
