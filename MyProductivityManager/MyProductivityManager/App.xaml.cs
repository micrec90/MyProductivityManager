﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyProductivityManager.Core.Context;
using MyProductivityManager.Core.Interfaces;
using MyProductivityManager.Core.Repositories;
using MyProductivityManager.Core.Services;
using MyProductivityManager.Core.ViewModels;
using System.Configuration;
using System.Windows;

namespace MyProductivityManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _servicesProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<FinanceViewModel>();
            services.AddSingleton<TasksViewModel>();
            services.AddSingleton<MarkdownEditorViewModel>();
            services.AddSingleton<RecipeOrganizerMealPlannerViewModel>();
            services.AddSingleton<TimeTrackerViewModel>();
            services.AddSingleton<MusicPlayerPlaylistManagerViewModel>();
            services.AddSingleton<WeatherDashboardViewModel>();
            services.AddSingleton<HabitTrackerViewModel>();
            services.AddSingleton<WatermarkEditorViewModel>();

            services.AddSingleton<FlashcardStudyViewModel>();
            services.AddSingleton<DataVisualizationDashboardViewModel>();
            services.AddSingleton<InventoryManagementSystemViewModel>();
            services.AddSingleton<ImageEditorMemeGeneratorViewModel>();
            services.AddSingleton<ChatClientViewModel>();
            services.AddSingleton<ProductivityViewModel>();
            services.AddSingleton<CodeSnippetManagerViewModel>();
            services.AddSingleton<PortfolioManagerViewModel>();


            services.AddTransient<ProjectDialogViewModel>();
            services.AddTransient<TaskDialogViewModel>();

            services.AddTransient<YesNoDialogViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDialogService, DialogService>();

            services.AddSingleton<Func<Type, ViewModel>>(servicesProvider => viewModelType => (ViewModel)servicesProvider.GetRequiredService(viewModelType));
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IProjectsTasksRepository, ProjectsTasksRepository>();

            _servicesProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _servicesProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
