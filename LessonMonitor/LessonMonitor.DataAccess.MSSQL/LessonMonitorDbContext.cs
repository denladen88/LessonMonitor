using LessonMonitor.DataAccess.MSSQL.Configurations;
using LessonMonitor.DataAccess.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LessonMonitor.DataAccess.MSSQL
{
	public class  LessonMonitorDbContext : DbContext
	{
		public LessonMonitorDbContext(DbContextOptions<LessonMonitorDbContext> options) : base(options)
		{
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

		public DbSet<Lesson> Lessons { get; set; }
		public DbSet<Homework> Homeworks { get; set; }
		public DbSet<Member> Members { get; set; }
		public DbSet<GithubAccount> GithubAccounts { get; set; }
		//public DbSet<MemberHomework> MemberHomeworks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new HomeworkConfiguration());
			modelBuilder.ApplyConfiguration(new LessonConfiguration());
			modelBuilder.ApplyConfiguration(new MemberConfiguration());
			modelBuilder.ApplyConfiguration(new GithubAccountConfiguration());

			base.OnModelCreating(modelBuilder);
		}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
