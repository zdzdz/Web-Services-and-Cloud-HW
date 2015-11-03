namespace StudentSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Collections.Generic;

    using Data;
    using Models;
    using StudentSystem.Models;

    public class StudentsController : ApiController
    {
        private IStudentsSystemData Data;

        public StudentsController()
            : this(new StudentsSystemData())
        {
        }

        public StudentsController(IStudentsSystemData data)
        {
            this.Data = data;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var students = this.Data.Students
                .All()
                .Select(StudentResponseModel.FromStudent)
                .ToList();

            return this.Ok(students);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var student = this.Data.Students
                .All()
                .Select(StudentResponseModel.FromStudent)
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return this.NotFound();
            }

            return this.Ok(student);
        }

        [HttpPost]
        public IHttpActionResult Post(StudentApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var studentForDb = new Student()
            {
                Name = model.Name,
                Number = model.Number,
                Courses = new List<Course>(),
                Homeworks = new List<Homework>()
            };

            this.Data.Students.Add(studentForDb);
            this.Data.SaveChanges();

            return this.Ok(model);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, StudentApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var studentFromDb = this.Data.Students
                .Find(id);

            studentFromDb.Name = model.Name;
            studentFromDb.Number = model.Number;

            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var student = this.Data.Students
                .Find(id);

            if (student == null)
            {
                return this.NotFound();
            }

            this.Data.Students.Delete(student);
            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult AddHomeworkToStudent(int studentId, int homeworkId)
        {
            var student = this.Data.Students
                .Find(studentId);

            if (student == null)
            {
                return this.NotFound();
            }

            var homework = this.Data.Homeworks
                .Find(homeworkId);

            if (homework == null)
            {
                return this.NotFound();
            }

            student.Homeworks.Add(homework);
            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult AddCourseToStudent(int studentId, int courseId)
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

            student.Courses.Add(course);
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}