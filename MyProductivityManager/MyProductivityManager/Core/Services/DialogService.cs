using MyProductivityManager.Core.Interfaces;
using MyProductivityManager.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyProductivityManager.Core.Services
{
    public class DialogService : ObservableObject, IDialogService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory;
        private Dictionary<Type, Type> _viewModelToViewMap = new Dictionary<Type, Type>();
        public DialogService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }
        public void RegisterDialog<TViewModel, TView>()
        {
            if (_viewModelToViewMap.ContainsKey(typeof(TViewModel)))
                return;
            _viewModelToViewMap.Add(typeof(TViewModel), typeof(TView));
        }
        public bool? ShowDialog<TViewModel>() where TViewModel : ViewModel
        {
            var viewModel = (TViewModel)_viewModelFactory(typeof(TViewModel));
            return ShowDialog(viewModel);
        }
        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
        {
            var vmType = viewModel.GetType();
            if (!_viewModelToViewMap.TryGetValue(vmType, out var viewType))
                throw new InvalidOperationException($"No dialog view mapped for {vmType.Name}");
            var dialog = (Window)Activator.CreateInstance(viewType)!;
            dialog.DataContext = viewModel;
            dialog.Owner = Application.Current.MainWindow;

            EventHandler closeEventHandler = null;
            closeEventHandler = (s, e) =>
            {
                //callback(dialog.DialogResult.ToString());
                _ = dialog.DialogResult;
                dialog.Closed -= closeEventHandler;
            };
            dialog.Closed += closeEventHandler;


            return dialog.ShowDialog();
        }
    }
}
