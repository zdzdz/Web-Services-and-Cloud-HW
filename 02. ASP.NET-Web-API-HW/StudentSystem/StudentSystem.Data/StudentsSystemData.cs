namespace StudentSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;

    using Models;
    using Contracts;

    public class StudentsSystemData : IStudentsSystemData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public StudentsSystemData()
            : this(new StudentsSystemDbContext())
        {
        }

        public StudentsSystemData(StudentsSystemDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }

        public IRepository<Course> Courses
        {
            get { return this.GetRepository<Course>(); }
        }

        public IRepository<Homework> Homeworks
        {
            get { return this.GetRepository<Homework>(); }
        }

        public IRepository<Student> Students
        {
            get { return this.GetRepository<Student>(); }
        }
    }
}