namespace StudentSystem.Data
{
    using Models;
    using Contracts;

    public interface IStudentsSystemData
    {
        IRepository<Course> Courses { get; }

        IRepository<Homework> Homeworks { get; }

        IRepository<Student> Students { get; }

        int SaveChanges();
    }
}