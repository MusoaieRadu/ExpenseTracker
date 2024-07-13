using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class ExpenseService
    {
        private readonly ExpenseTrackerContext _context;
        public ExpenseService(ExpenseTrackerContext context) {
            _context = context;
        }
        public List<Expense> GetExpenseList()
        {
            return _context.Expenses
                    .Include(expense => expense.Category)
                    .ToList();
        }
        public void Delete(int id)
        {
            try
            {
                Expense? expense = _context.Expenses.Find(id);
                if (expense == null) throw new ArgumentNullException();
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
                this.NotifyExpenseChange();
            }
            catch { throw; }
        }
        public void Post(Expense expense)
        {
            expense.Date = expense.Date.ToUniversalTime();
            _context.Expenses.Add(expense);
            _context.SaveChanges();
            this.NotifyExpenseChange();
        }
        public void Update(Expense expense)
        {
            expense.Date = expense.Date.ToUniversalTime();
            _context.Entry(expense).State = EntityState.Modified;
            _context.SaveChanges();
            this.NotifyExpenseChange();
        }
        public Expense Get(int id)
        {
            try
            {
                Expense? expense = _context.Expenses.Find(id);
                if (expense == null) throw new ArgumentNullException();
                return expense;
            }
            catch { throw; }
        }
        public event Action? OnExpenseChange;
        private void NotifyExpenseChange() => OnExpenseChange?.Invoke();
    }
}
