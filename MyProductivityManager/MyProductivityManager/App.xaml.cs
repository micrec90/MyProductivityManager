using Microsoft.Extensions.DependencyInjection;
using MyProductivityManager.Core.Interfaces;
using MyProductivityManager.Core.Services;
using MyProductivityManager.Core.ViewModels;
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
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModel>>(servicesProvider => viewModelType => (ViewModel)servicesProvider.GetRequiredService(viewModelType));

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
