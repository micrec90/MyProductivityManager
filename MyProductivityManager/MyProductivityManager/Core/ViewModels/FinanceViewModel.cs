using MyProductivityManager.Core.Models;
using MyProductivityManager.Core.Models.Finance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.ViewModels
{
    public class FinanceViewModel : ViewModel
    {
        public ObservableCollection<FinancialTransaction> AllTransactions { get; set; } = new ObservableCollection<FinancialTransaction>();
        public ObservableCollection<FinancialTransaction> FilteredTransactions { get; set; } = new ObservableCollection<FinancialTransaction>();

        public ObservableCollection<MonthEnum> Months { get; set; }
        public ObservableCollection<TransactionType> TransactionTypes { get; set; }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }
        private MonthEnum _selectedMonth;
        public MonthEnum SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value;
                FilterData();
                OnPropertyChanged();
            }
        }
        private TransactionType _selectedTransactionType;
        public TransactionType SelectedTransactionType
        {
            get { return _selectedTransactionType; }
            set
            {
                _selectedTransactionType = value;
                FilterData();
                OnPropertyChanged();
            }
        }
        private FinancialTransaction _selectedItem;
        public FinancialTransaction SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        public decimal TotalIncome => FilteredTransactions.Where(t => t.Amount >= 0).Sum(t => t.Amount);
        public decimal TotalExpenses => FilteredTransactions.Where(t => t.Amount < 0).Sum(t => Math.Abs(t.Amount));
        public decimal Total => FilteredTransactions.Sum(t => t.Amount);

        public RelayCommand AddTransactionCommand { get; set; }
        public RelayCommand EditTransactionCommand { get; set; }
        public RelayCommand DeleteTransactionCommand { get; set; }

        public FinanceViewModel()
        {
            AddTransactionCommand = new RelayCommand(obj => AddTransaction(), obj => !string.IsNullOrEmpty(Description));
            DeleteTransactionCommand = new RelayCommand(obj => DeleteTransaction(), obj => SelectedItem != null);
            TransactionTypes = new ObservableCollection<TransactionType>(Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>());
            Months = new ObservableCollection<MonthEnum>(Enum.GetValues(typeof(MonthEnum)).Cast<MonthEnum>());
        }

        private void FilterData()
        {
            var filtered = AllTransactions.AsEnumerable();

            if (SelectedMonth != MonthEnum.All)
            {
                int monthNumber = Months.IndexOf(SelectedMonth);
                filtered = filtered.Where(t => t.Date.Month == monthNumber);
            }
            if (SelectedTransactionType == TransactionType.Income)
                filtered = filtered.Where(t => t.Amount >= 0);
            else if (SelectedTransactionType == TransactionType.Expense)
                filtered = filtered.Where(t => t.Amount < 0);

            FilteredTransactions.Clear();
            foreach (var transaction in filtered)
            {
                FilteredTransactions.Add(transaction);
            }

            OnPropertyChanged(nameof(TotalIncome));
            OnPropertyChanged(nameof(TotalExpenses));
            OnPropertyChanged(nameof(Total));
        }
        private void AddTransaction()
        {
            FinancialTransaction transaction = new FinancialTransaction()
            {
                Description = Description,
                Amount = Amount,
                Type = Amount >= 0 ? TransactionType.Income : TransactionType.Expense
            };
            AllTransactions.Add(transaction);
            FilterData();
        }
        private void EditTransaction()
        {

        }
        private void DeleteTransaction()
        {
            AllTransactions.Remove(SelectedItem);
            FilterData();
        }
    }
}
