﻿using Microsoft.EntityFrameworkCore;
using MyProductivityManager.Core.Models.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    }
}
