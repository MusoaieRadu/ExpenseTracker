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
    }
}
