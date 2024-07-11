using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateOnly Date { get; set; }
        public int Amount { get; set; }
        public bool Planned { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
