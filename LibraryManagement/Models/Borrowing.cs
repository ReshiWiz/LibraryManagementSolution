using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
	public class Borrowing
	{
		[Key]
		public int BorrowingId { get; set; }
		[ForeignKey("Book")]
		public int BookId { get; set; }
		[ForeignKey("Student")]
		public int StudId { get; set; }
		public string? BookImg { get; set; }
		public DateTime DateBorrowed { get; set; }
		public DateTime DateReturn { get; set; }

		public virtual Book? Book { get; set; }
		public virtual Student? Student { get; set; }
	}
}
