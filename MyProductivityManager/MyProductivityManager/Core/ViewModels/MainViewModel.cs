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
        public RelayCommand NavigationToMarkdownEditorCommand { get; set; }
        public RelayCommand NavigationToRecipeOrganizerMealPlannerCommand { get; set; }
        public RelayCommand NavigationToTimeTrackerCommand { get; set; }
        public RelayCommand NavigationToMusicPlayerPlaylistManagerCommand { get; set; }
        public RelayCommand NavigationToWeatherDashboardCommand { get; set; }
        public RelayCommand NavigationToHabitTrackerCommand { get; set; }
        public RelayCommand NavigationToWatermarkEditorCommand { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationToHomeCommand = new RelayCommand(obj => { NavigationService.NavigateTo<HomeViewModel>(); }, obj => true);
            NavigationToFinanceCommand = new RelayCommand(obj => { NavigationService.NavigateTo<FinanceViewModel>(); }, obj => true);
            NavigationToTasksCommand = new RelayCommand(obj => { NavigationService.NavigateTo<TasksViewModel>(); }, obj => true);

            NavigationToMarkdownEditorCommand = new RelayCommand(obj => { NavigationService.NavigateTo<MarkdownEditorViewModel>(); }, obj => true);
            NavigationToRecipeOrganizerMealPlannerCommand = new RelayCommand(obj => { NavigationService.NavigateTo<RecipeOrganizerMealPlannerViewModel>(); }, obj => true);
            NavigationToTimeTrackerCommand = new RelayCommand(obj => { NavigationService.NavigateTo<TimeTrackerViewModel>(); }, obj => true);
            NavigationToMusicPlayerPlaylistManagerCommand = new RelayCommand(obj => { NavigationService.NavigateTo<MusicPlayerPlaylistManagerViewModel>(); }, obj => true);
            NavigationToWeatherDashboardCommand = new RelayCommand(obj => { NavigationService.NavigateTo<WeatherDashboardViewModel>(); }, obj => true);
            NavigationToHabitTrackerCommand = new RelayCommand(obj => { NavigationService.NavigateTo<HabitTrackerViewModel>(); }, obj => true);
            NavigationToWatermarkEditorCommand = new RelayCommand(obj => { NavigationService.NavigateTo<WatermarkEditorViewModel>(); }, obj => true);
        }
    }
}
