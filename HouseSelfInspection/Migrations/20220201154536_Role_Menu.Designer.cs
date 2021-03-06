// <auto-generated />
using System;
using HouseSelfInspection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HouseSelfInspection.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220201154536_Role_Menu")]
    partial class Role_Menu
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HouseSelfInspection.Models.ActivityModel", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityAudienceUser");

                    b.Property<DateTime>("ActivityDate");

                    b.Property<string>("ActivityDescription");

                    b.Property<string>("ActivityTitle");

                    b.Property<int>("ActivityUser");

                    b.HasKey("ActivityId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.CommentModel", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityId");

                    b.Property<DateTime>("CommentDateTime");

                    b.Property<string>("CommentDesciption");

                    b.Property<int>("CommentUserId");

                    b.HasKey("CommentID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.HouseModel", b =>
                {
                    b.Property<int>("HouseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("House_number")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("House_size")
                        .HasMaxLength(10);

                    b.Property<string>("House_type")
                        .HasMaxLength(25);

                    b.Property<string>("ImgPath");

                    b.Property<string>("Postal_code")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("Suburb")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("HouseId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.ImageModel", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HouseId");

                    b.Property<string>("ImageUrl");

                    b.Property<int>("SectionId");

                    b.Property<int>("SubmittedBy");

                    b.Property<DateTime>("SubmittedDate");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.InspectionScheduleModel", b =>
                {
                    b.Property<int>("InspectionScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HouseId");

                    b.Property<DateTime>("Inspection_date");

                    b.Property<string>("Inspection_status");

                    b.Property<int>("TenantId");

                    b.HasKey("InspectionScheduleId");

                    b.ToTable("InspectionSchedules");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.InspectionSubmitModel", b =>
                {
                    b.Property<int>("InspectionUploadId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HouseId");

                    b.Property<int>("ImageId");

                    b.Property<DateTime>("InspectionSubmittedDate");

                    b.Property<int>("SectionId");

                    b.Property<int>("TenantId");

                    b.HasKey("InspectionUploadId");

                    b.ToTable("InspectionSubmits");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.MenuModel", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MenuIcon");

                    b.Property<string>("MenuName");

                    b.Property<string>("MenuUrl");

                    b.Property<int>("ParentId");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.RoleMenuModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MenuId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("RoleMenus");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.Static_Models.HouseSectionModel", b =>
                {
                    b.Property<int>("HouseSectionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HouseSectionName")
                        .IsRequired();

                    b.HasKey("HouseSectionId");

                    b.ToTable("HouseSections");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.Static_Models.HouseTypeModel", b =>
                {
                    b.Property<int>("HouseTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HouseType")
                        .IsRequired();

                    b.HasKey("HouseTypeId");

                    b.ToTable("HouseTypes");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.Static_Models.RoleModel", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .IsRequired();

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.Static_Models.StateModel", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("State")
                        .IsRequired();

                    b.HasKey("StateId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.TenantModel", b =>
                {
                    b.Property<int>("TenantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TenantId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<int>("HouseId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("house_address")
                        .HasMaxLength(100);

                    b.HasKey("TenantId");

                    b.HasIndex("HouseId");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("HouseSelfInspection.Models.TenantModel", b =>
                {
                    b.HasOne("HouseSelfInspection.Models.HouseModel", "House")
                        .WithMany("Tenants")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
