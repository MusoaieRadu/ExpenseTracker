using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using System.Reflection.Metadata.Ecma335;

namespace ExpenseTracker.Services
{
    public class CategoryService
    {
        private readonly ExpenseTrackerContext _context;
        public CategoryService(ExpenseTrackerContext context)
        {
            _context = context; 
        }
        public List<Category> GetCategories() => _context.Categories.ToList();
        public void Post(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            this.NotifyCategoryChanged();
        }
        public void Delete(Category category)
        {
            if (category == null) return;
            _context.Categories.Remove(category);
            _context.SaveChanges();
            this.NotifyCategoryChanged();
        }
        public event Action OnCategoryChanged = null!;
        private void NotifyCategoryChanged() => OnCategoryChanged.Invoke();
    }
}
