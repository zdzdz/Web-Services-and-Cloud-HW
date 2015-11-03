namespace StudentSystem.Data
{
    using System.Data.Entity;

    using Models;
    using Migrations;

    public class StudentsSystemDbContext : DbContext
    {
        public StudentsSystemDbContext()
            : base("StudentSystemConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentsSystemDbContext, Configuration>());
        }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Homework> Homeworks { get; set; }

        public IDbSet<Student> Students { get; set; }

        public static StudentsSystemDbContext Create()
        {
            return new StudentsSystemDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}