using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using TaskManagerApi.Entities;
using TaskManagerApi.Helpers;

namespace TaskManagerApi.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();   // создаем бд с новой схемой
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            string schema = "AspNetIdentity";

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User", schema: schema);
            });

            modelBuilder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable(name: "Role", schema: schema);
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaim", schema);
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogin", schema);
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaim", schema);
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRole", schema);
            });

            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserToken", schema);
            });

            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Name = RolesEnum.Teacher,
                NormalizedName = RolesEnum.Teacher.ToUpper(),
                Id = 1,
                ConcurrencyStamp = "1"
            });

            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Name = RolesEnum.Student,
                NormalizedName = RolesEnum.Student.ToUpper(),
                Id = 2,
                ConcurrencyStamp = "2"
            });

            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Name = RolesEnum.Manager,
                NormalizedName = RolesEnum.Manager.ToUpper(),
                Id = 3,
                ConcurrencyStamp = "3"
            });
            //modelBuilder.Entity<User>()
            //            .HasOne(u => u.Role)
            //            .WithMany(c => c.Users)
            //            .HasForeignKey(u => u.RoleInfoKey); 

            //modelBuilder.Entity<Priority>().HasData(
            //    new Priority { Id = 1, Name = "Наивысший" },
            //    new Priority { Id = 2, Name = "Высокий" },
            //    new Priority { Id = 3, Name = "Средний" },
            //    new Priority { Id = 4, Name = "Низкий" },
            //    new Priority { Id = 5, Name = "Самый низкий" }
            //);
            //modelBuilder.Entity<Status>().HasData(
            //    new Status { Id = 1, Name = "Открыта" },
            //    new Status { Id = 2, Name = "В работе" },
            //    new Status { Id = 3, Name = "Решена" },
            //    new Status { Id = 4, Name = "Переоткрыта" },
            //    new Status { Id = 5, Name = "Закрыта" }
            //);
            modelBuilder.Entity<Project>()
                .HasMany(c => c.Participants)
                .WithMany(s => s.Projects);
            modelBuilder.Entity<User>()
                .HasMany(c => c.ManagedProjects)
                .WithOne(e => e.Manager);
        }
        public DbSet<Group> Group { get; set; } = null!;
        public DbSet<Subject> Subject { get; set; } = null!;
        public DbSet<Project> Project { get; set; } = null!;
        public DbSet<TeamRole> TeamRole { get; set; } = null!;
        public DbSet<Status> Status { get; set; } = null!;
        public DbSet<Priority> Priority { get; set; } = null!;
        public DbSet<TaskEntity> Task { get; set; } = null!;
    }
}
