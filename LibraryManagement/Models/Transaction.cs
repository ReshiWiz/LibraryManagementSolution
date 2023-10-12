using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
	public class Transaction
	{
		[Key]
		public int TransId { get; set; }
		public string? TransName { get; set; }
		[ForeignKey("Borrowing")]
		public int BorrowingId { get; set; }
		[ForeignKey("Student")]
		public int StudId { get; set; }
		public DateTime TransDate { get; set; }

		public virtual Borrowing? Borrowing { get; set; }
		public virtual Student? Student { get; set; }
	}
}
