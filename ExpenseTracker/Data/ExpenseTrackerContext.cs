using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public class ExpenseTrackerContext : DbContext
    {
        public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options)
            :base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(new Category[]
                {
                    new Category(1, "Food"),
                    new Category(2, "Travel"),
                    new Category(3, "Entertainment"),
                    new Category(4, "Education"),
                    new Category(5, "Clothes"),
                    new Category(6, "House")
                });
        }
    }
}
