namespace StudentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Course
    {
        private ICollection<Student> students;
        private ICollection<Homework> homeworks;

        public Course()
        {
            this.students = new HashSet<Student>();
            this.homeworks = new HashSet<Homework>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage = "Course name should be shorter than 60 characters")]
        [Column("Course name")]
        public string Name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Course description should be long at least 10 characters")]
        [MaxLength(255, ErrorMessage = "Course description should be shorter than 255 characters")]
        [Column("Course description")]
        public string Description { get; set; }

        public string Materials { get; set; }

        public virtual ICollection<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
