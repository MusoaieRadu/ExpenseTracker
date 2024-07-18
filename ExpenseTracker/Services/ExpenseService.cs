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
                    .AsNoTracking()
                    .Include(expense => expense.Category)
                    .ToList();
        }
        public List<Expense> GetExpensesByCategory(string category)
        {
            return _context.Expenses
                    .AsNoTracking()
                    .Include(expense => expense.Category)
                    .Where(expense => expense.Category.Name == category)
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
            _context.Entry(expense).State = EntityState.Detached;
            this.NotifyExpenseChange();
        }
        public void Update(Expense expense)
        {
            expense.Date = expense.Date.ToUniversalTime();
            expense.Date = DateTime.SpecifyKind(expense.Date, DateTimeKind.Utc);
            _context.Entry(expense).State = EntityState.Modified; //track the change
            _context.SaveChanges(); //save 
            _context.Entry(expense).State = EntityState.Detached; //stop tracking the change
            this.NotifyExpenseChange(); //notify whoever listens to this change
        }
        public Expense Get(int id)
        {
            Expense? expense = _context.Expenses.AsNoTracking().FirstOrDefault(X => X.Id == id);
            return expense;
        }

        public event Action? OnExpenseChange;
        private void NotifyExpenseChange() => OnExpenseChange?.Invoke();
    }
}
