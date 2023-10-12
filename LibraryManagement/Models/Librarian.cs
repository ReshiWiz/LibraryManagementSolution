using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
	public class Librarian
	{
		[Key]
		public int LibrarianId { get; set; }
		[Display(Name = "First Name")]
		public string? Fname { get; set; }
		[Display(Name = "Last Name")]
		public string? Lname { get; set; }
		public string? Gender { get; set; }
		public int Age { get; set; }
		public string? ContactAdd { get; set; }
		public string? UserEmail { get; set; }
		public string? UserPass { get; set; }
		public virtual ICollection<Transaction>? Transactions { get; set; }

	}
}
