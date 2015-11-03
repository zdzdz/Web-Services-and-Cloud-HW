namespace StudentSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Collections.Generic;

    using Data;
    using StudentSystem.Models;
    using Models;

    public class CoursesController : ApiController
    {
        private IStudentsSystemData Data;

        public CoursesController()
            : this(new StudentsSystemData())
        {
        }

        public CoursesController(IStudentsSystemData data)
        {
            this.Data = data;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var courses = this.Data.Courses
                .All()
                .Select(CourseApiModel.FromCourse)
                .ToList();

            return this.Ok(courses);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var course = this.Data.Courses
                .All()
                .Select(CourseApiModel.FromCourse)
                .FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return this.NotFound();
            }

            return this.Ok(course);
        }

        [HttpPost]
        public IHttpActionResult Post(CourseApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var courseForDb = new Course()
            {
                Name = model.Name,
                Description = model.Description,
                Materials = model.Materials,
                Homeworks = new List<Homework>(),
                Students = new List<Student>()
            };

            this.Data.Courses.Add(courseForDb);
            this.Data.SaveChanges();

            return this.Ok(model);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, CourseApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var courseFromDb = this.Data.Courses
                .Find(id);

            courseFromDb.Name = model.Name;
            courseFromDb.Description = model.Description;
            courseFromDb.Materials = model.Materials;

            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var course = this.Data.Courses
                .Find(id);

            if (course == null)
            {
                return this.NotFound();
            }

            this.Data.Courses.Delete(course);
            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult AddHomeworkToCourse(int homeworkId, int courseId)
        {
            var homework = this.Data.Homeworks
                .Find(homeworkId);

            if (homework == null)
            {
                return this.NotFound();
            }

            var course = this.Data.Courses
                .Find(courseId);

            if (course == null)
            {
                return this.NotFound();
            }

            course.Homeworks.Add(homework);
            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult AddStudentToCourse(int studentId, int courseId)
        {
            var student = this.Data.Students
                .Find(studentId);

            if (student == null)
            {
                return this.NotFound();
            }

            var course = this.Data.Courses
                .Find(courseId);

            if (course == null)
            {
                return this.NotFound();
            }

            course.Students.Add(student);
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}