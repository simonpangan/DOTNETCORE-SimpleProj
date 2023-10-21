    using System.ComponentModel.DataAnnotations.Schema;

    namespace SimonProj.Models;

    public class Student
    {
        public int ID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }
    }