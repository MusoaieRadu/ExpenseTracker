using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class CategoryService
    {
        private readonly ExpenseTrackerContext _context;
        public CategoryService(ExpenseTrackerContext context)
        {
            _context = context;
        }
        public List<Category> GetCategories() {
            return _context.Categories.ToList();
         }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            this.NotifyStateChanged();
        }
        public void RemoveCategory(Category category)
        {
            if (category == null) return;
            _context.Categories.Remove(category);
            _context.SaveChanges();
            this.NotifyStateChanged();
        }
        public event Action onChange;
        private void NotifyStateChanged() => onChange.Invoke();
    }
}
