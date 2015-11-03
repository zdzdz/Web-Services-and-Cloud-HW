namespace StudentSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Models;
    using StudentSystem.Models;

    public class HomeworksController : ApiController
    {
        private IStudentsSystemData Data;

        public HomeworksController()
            : this(new StudentsSystemData())
        {
        }

        public HomeworksController(IStudentsSystemData data)
        {
            this.Data = data;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var homeworks = this.Data.Homeworks
                .All()
                .Select(HomeworkApiModel.FromHomework)
                .ToList();

            return this.Ok(homeworks);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var homework = this.Data.Homeworks
                .All()
                .Select(HomeworkApiModel.FromHomework)
                .FirstOrDefault(h => h.Id == id);

            if (homework == null)
            {
                return this.NotFound();
            }

            return this.Ok(homework);
        }

        [HttpPost]
        public IHttpActionResult Post(HomeworkApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            var homeworkForDb = new Homework()
            {
                Content = model.Content,
                TimeSent = model.TimeSent,
                CourseId = model.CourseId,
                StudentId = model.StudentId
            };

            this.Data.Homeworks.Add(homeworkForDb);
            this.Data.SaveChanges();

            return this.Ok(model);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, HomeworkApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var homeworkFromDb = this.Data.Homeworks
                .Find(id);

            homeworkFromDb.Content = model.Content;
            homeworkFromDb.TimeSent = model.TimeSent;
            homeworkFromDb.CourseId = model.CourseId;
            homeworkFromDb.StudentId = model.StudentId;

            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var homework = this.Data.Homeworks
                .Find(id);

            if (homework == null)
            {
                return this.NotFound();
            }

            this.Data.Homeworks.Delete(homework);
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}