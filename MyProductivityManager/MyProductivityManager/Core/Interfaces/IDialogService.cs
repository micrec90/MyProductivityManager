using MyProductivityManager.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Interfaces
{
    public interface IDialogService
    {
        public void RegisterDialog<TViewModel, TView>();
        bool? ShowDialog<TViewModel>() where TViewModel : ViewModel;
        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel;
    }
}
