using System.ComponentModel;

namespace ExpenseTracker.Data.Models
{
    public enum IncomeType
    {
        [Description("Salary")]
        Salary,
        [Description("Scholarship")]
        Scholarship, 
        [Description("Gift")]
        Gift,
        [Description("Lucky Winnings")]
        LuckyWinnings
    }
    public class Income
    {
        public int Id {  get; set; }
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public double Amount {  get; set; }
        public IncomeType Type {  get; set; }
    }
}
