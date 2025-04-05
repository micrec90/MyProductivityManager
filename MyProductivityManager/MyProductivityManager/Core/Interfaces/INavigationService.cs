using MyProductivityManager.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Interfaces
{
    public interface INavigationService
    {
        ViewModel CurrentViewModel { get; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModel;
    }
}
