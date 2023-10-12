using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
	public class Reports
	{
		[Key]
		public int ReportId { get; set; }
		[ForeignKey("Transaction")]
		public int TransId { get; set; }
		[ForeignKey("Borrowing")]
		public int BorrowingId { get; set; }
		public DateTime ReportDate { get; set; }
		public virtual Transaction? Transaction { get; set; }
		public virtual Borrowing? Borrowing { get; set; }
	}
}
