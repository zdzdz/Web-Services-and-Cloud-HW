namespace StudentSystem.Services.Models
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using StudentSystem.Models;
    using Common;

    public class StudentApiModel : IMapFrom<Student>
    {
        public static Expression<Func<Student, StudentApiModel>> FromStudent
        {
            get
            {
                return s => new StudentApiModel()
                {
                    Name = s.Name,
                    Number = s.Number
                };
            }
        }

        [Required]
        [MinLength(2, ErrorMessage = "Student name should be long at least 2 characters")]
        [MaxLength(150, ErrorMessage = "Student name should be shorter than 150 characters")]
        [Column("Student name")]
        public string Name { get; set; }

        public int Number { get; set; }
    }
}