namespace StudentSystem.Services.Models
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using StudentSystem.Models;
    using Common;

    public class CourseApiModel : IMapFrom<Course>
    {
        public static Expression<Func<Course, CourseApiModel>> FromCourse
        {
            get
            {
                return c => new CourseApiModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Materials = c.Materials
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage = "Course name should be shorter than 60 characters")]
        public string Name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Course description should be long at least 10 characters")]
        [MaxLength(255, ErrorMessage = "Course description should be shorter than 255 characters")]
        public string Description { get; set; }

        public string Materials { get; set; }
    }
}