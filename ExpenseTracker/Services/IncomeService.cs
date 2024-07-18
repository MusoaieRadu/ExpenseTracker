using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExpenseTracker.Services
{
    public class IncomeService
    {
        private readonly ExpenseTrackerContext _context;
        public IncomeService(ExpenseTrackerContext context)
        {
            _context = context;
        }
        public List<Income> GetIncomes() => _context.Incomes.AsNoTracking().ToList();
        public void Post(Income income)
        {
            income.Date = income.Date.ToUniversalTime();
            _context.Incomes.Add(income);
            _context.SaveChanges();
            _context.Entry(income).State = EntityState.Detached;
            this.NotifyIncomeChange();
        }
        public void Delete(int id)
        {
            try
            {
                Income? income = _context.Incomes.Find(id);
                if (income == null) throw new ArgumentNullException();
                _context.Incomes.Remove(income);
                _context.SaveChanges();
                this.NotifyIncomeChange();
            }
            catch { throw; }
        }
        public void Update(Income income)
        {
            income.Date = income.Date.ToUniversalTime();
            income.Date = DateTime.SpecifyKind(income.Date, DateTimeKind.Utc);
            _context.Entry(income).State = EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(income).State = EntityState.Detached;
            this.NotifyIncomeChange();
        }
        public Income Get(int id)
        {
            Income? income = _context.Incomes.AsNoTracking().FirstOrDefault(x=> x.Id == id);
            return income;
        }
        public event Action? OnIncomeChange;
        private void NotifyIncomeChange() => OnIncomeChange?.Invoke();
        public string IncomeTypeToString(IncomeType type)
        {
            Type enumType = type.GetType();
            MemberInfo[] memberInfo = enumType.GetMember(type.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return type.ToString();
        }
    }
}
