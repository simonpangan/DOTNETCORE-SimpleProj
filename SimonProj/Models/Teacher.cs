using System.ComponentModel.DataAnnotations;
using SimonProj.Models.Validations;

namespace SimonProj.Models;

public class Teacher
{
    public int ID { get; set; }

    [Required]
    [StringLength(8, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
    public string LastName { get; set; }

    [Required]
    [StringLength(8, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "The Joined Date is required")]
    [DateNotTomorrow(ErrorMessage = "Joined date must from the future")]
    public DateTime? JoinedDate { get; set; }
}