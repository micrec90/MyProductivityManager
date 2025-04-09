using MyProductivityManager.Core.Models.Finance;
using MyProductivityManager.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Interfaces
{
    public interface IFinancialTransactionRepository
    {
        Task<List<FinancialTransaction>> GetAll();
        Task<List<FinancialTransaction>> GetAll(FinancialTransactionQueryObject financialTransactionQueryObject);
        Task<FinancialTransaction?> GetById(int id);
        Task<FinancialTransaction> Add(FinancialTransaction financialTransaction);
        Task<FinancialTransaction?> Edit(int id, FinancialTransaction financialTransaction);
        Task<FinancialTransaction?> Delete(int id);
        Task<bool> FinancialTransactionExists(int id);
    }
}
