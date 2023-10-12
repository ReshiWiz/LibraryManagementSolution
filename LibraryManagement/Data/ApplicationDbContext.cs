using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Student> Students { get; set; }
		public DbSet<Librarian> Librarian { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Borrowing> BookBorrowing { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Reports> Reports { get; set; }
	}
}
