using MyProductivityManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private INavigationService _navigationService;

        public INavigationService NavigationService
        {
            get { return _navigationService; }
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand NavigationToHomeCommand { get; set; }
        public RelayCommand NavigationToFinanceCommand { get; set; }
        public RelayCommand NavigationToTasksCommand { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationToHomeCommand = new RelayCommand(obj => { NavigationService.NavigateTo<HomeViewModel>(); }, obj => true);
            NavigationToFinanceCommand = new RelayCommand(obj => { NavigationService.NavigateTo<FinanceViewModel>(); }, obj => true);
            NavigationToTasksCommand = new RelayCommand(obj => { NavigationService.NavigateTo<TasksViewModel>(); }, obj => true);
        }
    }
}
