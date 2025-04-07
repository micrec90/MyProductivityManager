using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Models.Finance
{
    public class FinancialTransaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TransactionType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount {  get; set; }
        [NotMapped]
        public bool IsIncome => Amount >= 0;
    }
}
