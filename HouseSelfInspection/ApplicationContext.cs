using HouseSelfInspection.Models;
using HouseSelfInspection.Models.Static_Models;
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
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<InspectionScheduleModel> InspectionSchedules { get; set; }
        public DbSet<InspectionSubmitModel> InspectionSubmits { get; set; }
        public DbSet<TenantModel> Tenants { get; set; }
        public DbSet<HouseSectionModel> HouseSections { get; set; }
        public DbSet<HouseTypeModel> HouseTypes { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<StateModel> States { get; set; }
        public DbSet<ActivityModel> Activities { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<MenuModel> Menus { get; set; }
        public DbSet<RoleMenuModel> RoleMenus { get; set; }

        public DbSet<FeedbackModel> Feedbacks { get; set; }                                                            
        public DbSet<ConnectionsModel> Connections { get; set; }

    }
}
