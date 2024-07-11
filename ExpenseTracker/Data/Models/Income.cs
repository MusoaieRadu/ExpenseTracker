namespace ExpenseTracker.Data.Models
{
    public enum IncomeType
    {
        Salary, Scholarship, Gift, LuckyWinnings
    }
    public class Income
    {
        public int Id {  get; set; }
        public string Title { get; set; } = null!;
        public DateOnly Date { get; set; }
        public int Amount {  get; set; }
        public IncomeType Type {  get; set; }
    }
}
