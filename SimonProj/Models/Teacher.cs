namespace SimonProj.Models;

public class Teacher
{
    public int ID { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public DateTime? JoinedDate { get; set; }

    public ICollection<Student> Students { get; set; }
}