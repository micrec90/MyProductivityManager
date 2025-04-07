using MyProductivityManager.Core.Models.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Queries
{
    public class FinancialTransactionQueryObject
    {
        public DateTime? Date { get; set; } = DateTime.Now;
        public TransactionType? Type { get; set; }
        public string? Description { get; set; } = string.Empty;
        public decimal? Amount { get; set; }
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
