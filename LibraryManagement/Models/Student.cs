using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
	public class Student
	{
		[Key]
		public int StudId { get; set; }
		[Display(Name = "First Name")]
		public string? Fname { get; set; }
		[Display(Name = "Last Name")]
		public string? Lname { get; set; }
		public string? Gender { get; set; }
		public int Age { get; set; }
		public string? ContactAdd { get; set; }
		[Display(Name = "Student Email")]
		public string? StudEmail { get; set; }
		[Display(Name = "Student Pass")]
		public string? StudPass { get; set; }

		public virtual ICollection<Borrowing>? Borrowings { get; set; }
	}
}
