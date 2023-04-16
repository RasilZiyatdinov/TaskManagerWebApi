using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models.Enums;

namespace TaskManagerWebApi.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();   // создаем бд с новой схемой
        }

        /// <summary>
        /// Создание схемы БД
        /// </summary>
        /// <param name="modelBuilder"></param>
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

            modelBuilder.Entity<Priority>().HasData(
                new Priority { Id = 1, Name = "Наивысший" },
                new Priority { Id = 2, Name = "Высокий" },
                new Priority { Id = 3, Name = "Средний" },
                new Priority { Id = 4, Name = "Низкий" },
                new Priority { Id = 5, Name = "Самый низкий" }
            );
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Открыта" },
                new Status { Id = 2, Name = "В работе" },
                new Status { Id = 3, Name = "Решена" },
                new Status { Id = 4, Name = "Переоткрыта" },
                new Status { Id = 5, Name = "Закрыта" }
            );
            modelBuilder.Entity<RequestStatus>().HasData(
                new RequestStatus { Id = 1, Name = "Принято" },
                new RequestStatus { Id = 2, Name = "Запрошено" },
                new RequestStatus { Id = 3, Name = "Отклонено" }
            );
            modelBuilder.Entity<TeamRole>().HasData(
                new TeamRole { Id = 1, Name = "Разработчик" },
                new TeamRole { Id = 2, Name = "Аналитик" },
                new TeamRole { Id = 3, Name = "Архитектор" },
                new TeamRole { Id = 4, Name = "Дизайнер" }
            );

            modelBuilder.Entity<Project>()
                .HasMany(c => c.RequestedParticipants)
                .WithMany(s => s.Projects);

            modelBuilder.Entity<User>()
                .HasMany(c => c.ManagedProjects)
                .WithOne(e => e.Manager);

            modelBuilder.Entity<Project>()
                .HasMany(c => c.TeamRoles)
                .WithOne(e => e.Project);

            modelBuilder
                .Entity<Project>()
                .HasMany(c => c.RequestedParticipants)
                .WithMany(s => s.Projects)
                .UsingEntity<Request>(
                j => j
                    .HasOne(pt => pt.Student)
                    .WithMany(t => t.Requests)
                    .HasForeignKey(pt => pt.StudentId),
                j => j
                    .HasOne(pt => pt.Project)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(pt => pt.ProjectId),
                j =>
                {
                    j.HasKey(t => new { t.ProjectId, t.StudentId });
                    j.ToTable("StudentProject");
                });


            modelBuilder
                .Entity<TaskEntity>()
                .HasMany(c => c.Students)
                .WithMany(s => s.TaskEntities)
                .UsingEntity<StudentTask>(
                   j => j
                    .HasOne(pt => pt.Student)
                    .WithMany(t => t.StudentTasks)
                    .HasForeignKey(pt => pt.StudentId),
                j => j
                    .HasOne(pt => pt.Task)
                    .WithMany(p => p.StudentTasks)
                    .HasForeignKey(pt => pt.TaskId),
                j =>
                {
                    j.HasKey(t => new { t.TaskId, t.StudentId });
                    j.ToTable("StudentTask");
                });


            modelBuilder
                .Entity<TaskEntity>()
                .HasMany(c => c.Students)
                .WithMany(s => s.TaskEntities)
                .UsingEntity<Comment>(
                   j => j
                    .HasOne(pt => pt.User)
                    .WithMany(t => t.Comments)
                    .HasForeignKey(pt => pt.UserId),
                j => j
                    .HasOne(pt => pt.Task)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(pt => pt.TaskId),
                j =>
                {
                    j.HasKey(t => new { t.TaskId, t.UserId });
                    j.ToTable("Comment");
                });

            modelBuilder
                .Entity<Mark>().HasKey( t => new {t.ProjectId, t.UserId});

            //modelBuilder.Entity<StudentTask>()
            //    .HasKey(e => new { e.StudentId, e.TaskId });

            //modelBuilder.Entity<StudentTask>()
            //    .HasOne(e => e.Student)
            //    .WithMany(e => e.StudentTasks)
            //    .HasForeignKey(e => e.StudentId);

            //modelBuilder.Entity<StudentTask>()
            //    .HasOne(e => e.Task)
            //    .WithMany(e => e.StudentTasks)
            //    .HasForeignKey(e => e.TaskId);

            //modelBuilder.Entity<StudentTask>()
            //    .HasOne(e => e.Role)
            //    .WithMany(e => e.StudentTasks)
            //    .HasForeignKey(e => e.RoleId);


        }
        /// <summary>
        /// Группы
        /// </summary>
        public DbSet<Group> Group { get; set; } = null!;

        /// <summary>
        /// Дисциплины
        /// </summary>
        public DbSet<Subject> Subject { get; set; } = null!;

        /// <summary>
        /// Проекты
        /// </summary>
        public DbSet<Project> Project { get; set; } = null!;

        /// <summary>
        /// Роли в команде
        /// </summary>
        public DbSet<TeamRole> TeamRole { get; set; } = null!;

        /// <summary>
        /// Статусы задач
        /// </summary>
        public DbSet<Status> Status { get; set; } = null!;

        /// <summary>
        /// Приоритеты задач
        /// </summary>
        public DbSet<Priority> Priority { get; set; } = null!;

        /// <summary>
        /// Задачи
        /// </summary>
        public DbSet<TaskEntity> Task { get; set; } = null!;

        /// <summary>
        /// Статусы заявок
        /// </summary>
        public DbSet<RequestStatus> RequestStatus { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public DbSet<RoleProject> RoleProject { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Request> StudentProject { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public DbSet<StudentTask> StudentTask { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Mark> Mark { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Comment> Comment { get; set; } = null!;
    }
}
