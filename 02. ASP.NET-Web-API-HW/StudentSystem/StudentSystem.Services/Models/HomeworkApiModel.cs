namespace StudentSystem.Services.Models
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using StudentSystem.Models;
    using Common;

    public class HomeworkApiModel : IMapFrom<Homework>
    {
        public static Expression<Func<Homework, HomeworkApiModel>> FromHomework
        {
            get
            {
                return c => new HomeworkApiModel()
                {
                    Id = c.Id,
                    Content = c.Content,
                    TimeSent = c.TimeSent,
                    CourseId = c.CourseId,
                    StudentId = c.StudentId
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime TimeSent { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }
    }
}