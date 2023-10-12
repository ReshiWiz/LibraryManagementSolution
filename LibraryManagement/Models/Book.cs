using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
	public class Book
	{
		[Key]
		public int BookId { get; set; }
		[Display(Name = "Book Title")]
		public string? BkTitle { get; set; }
		[Display(Name = "Book Name")]
		public string? BkName { get; set; }
		public string? Publisher { get; set; }
		public string? Author { get; set; }
		public string? BookImg { get; set; }
		[Display(Name = "Book Num")]
		public int BkNum { get; set; }
		public DateTime PubDate { get; set; }

		public virtual ICollection<Borrowing>? Borrowings { get; set; }
	}
}
