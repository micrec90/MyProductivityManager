using MyProductivityManager.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyProductivityManager.Core.Views
{
    /// <summary>
    /// Interaction logic for ProjectDialogWindow.xaml
    /// </summary>
    public partial class ProjectDialogWindow : Window
    {
        public ProjectDialogWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ProjectDialogViewModel viewModel)
            {
                viewModel.RequestClose += RequestClose;
            }
        }
        private void RequestClose(bool? result)
        {
            DialogResult = result;
            Close();
        }
    }
}
