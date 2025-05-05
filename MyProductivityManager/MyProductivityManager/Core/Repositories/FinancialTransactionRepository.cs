using Microsoft.EntityFrameworkCore;
using MyProductivityManager.Core.Context;
using MyProductivityManager.Core.Interfaces;
using MyProductivityManager.Core.Models.Finance;
using MyProductivityManager.Core.Queries;

namespace MyProductivityManager.Core.Repositories
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly ApplicationDBContext _context;
        public FinancialTransactionRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<FinancialTransaction> Add(FinancialTransaction financialTransaction)
        {
            await _context.FinancialTransactions.AddAsync(financialTransaction);
            await _context.SaveChangesAsync();
            return financialTransaction;
        }

        public async Task<FinancialTransaction?> Delete(int id)
        {
            var financialTransaction = await _context.FinancialTransactions.FirstOrDefaultAsync(x => x.Id == id);

            if (financialTransaction == null)
                return null;

            _context.FinancialTransactions.Remove(financialTransaction);
            await _context.SaveChangesAsync();

            return financialTransaction;
        }

        public async Task<FinancialTransaction?> Edit(int id, FinancialTransaction financialTransaction)
        {
            var financialTransactionToEdit = await _context.FinancialTransactions.FirstOrDefaultAsync(x => x.Id == id);

            if (financialTransactionToEdit == null)
                return null;

            financialTransactionToEdit.Description = financialTransaction.Description;
            financialTransactionToEdit.Amount = financialTransaction.Amount;
            financialTransactionToEdit.Type = financialTransaction.Type;
            financialTransaction.Date = financialTransaction.Date;

            _context.Entry(financialTransactionToEdit).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return financialTransactionToEdit;
        }

        public async Task<bool> FinancialTransactionExists(int id)
        {
            return await _context.FinancialTransactions.AnyAsync(e => e.Id == id);
        }
        public async Task<List<FinancialTransaction>> GetAll()
        {
            return await _context.FinancialTransactions.ToListAsync();
        }
        public async Task<List<FinancialTransaction>> GetAll(FinancialTransactionQueryObject financialTransactionQueryObject)
        {
            var financialTransactions = _context.FinancialTransactions.AsQueryable();

            if (financialTransactionQueryObject == null)
                return await financialTransactions.ToListAsync();
            if (!string.IsNullOrWhiteSpace(financialTransactionQueryObject.Description))
            {
                financialTransactions = financialTransactions.Where(u => u.Description.Contains(financialTransactionQueryObject.Description));
            }
            if (financialTransactionQueryObject.Amount != null)
            {
                financialTransactions = financialTransactions.Where(u => u.Amount == financialTransactionQueryObject.Amount.Value);
            }
            if (!string.IsNullOrWhiteSpace(financialTransactionQueryObject.SortBy))
            {
                if (financialTransactionQueryObject.SortBy.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    financialTransactions = financialTransactionQueryObject.IsDescending ? financialTransactions.OrderByDescending(x => x.Description) : financialTransactions.OrderBy(x => x.Description);
                }
                if (financialTransactionQueryObject.SortBy.Equals("Amount", StringComparison.OrdinalIgnoreCase))
                {
                    financialTransactions = financialTransactionQueryObject.IsDescending ? financialTransactions.OrderByDescending(x => x.Amount) : financialTransactions.OrderBy(x => x.Amount);
                }
            }
            if (financialTransactionQueryObject.Date != null)
            {
                financialTransactions = financialTransactions.Where(u => u.Date.Date == financialTransactionQueryObject.Date.Value.Date);
            }
            if(financialTransactionQueryObject.Type != null)
            {
                financialTransactions = financialTransactions.Where(u => u.Type == financialTransactionQueryObject.Type.Value);
            }
            int skip = (financialTransactionQueryObject.PageNumber - 1) * financialTransactionQueryObject.PageSize;

            return await financialTransactions.Skip(skip).Take(financialTransactionQueryObject.PageSize).ToListAsync();
        }

        public async Task<FinancialTransaction?> GetById(int id)
        {
            return await _context.FinancialTransactions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
