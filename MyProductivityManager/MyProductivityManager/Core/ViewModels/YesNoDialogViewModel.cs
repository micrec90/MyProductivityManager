using MyProductivityManager.Core.Models.ProjectTasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyProductivityManager.Core.ViewModels
{
    public class YesNoDialogViewModel : ViewModel
    {
        public RelayCommand YesCommand { get; set; }
        public RelayCommand NoCommand { get; set; }
        public YesNoDialogViewModel()
        {
            YesCommand = new RelayCommand(obj => { Yes(obj); }, obj => true);
            NoCommand = new RelayCommand(obj => { No(obj); }, obj => true);
        }
        private void Yes(object param)
        {
            ((Window)param).DialogResult = true;
            ((Window)param).Close();
        }
        private void No(object param)
        {
            ((Window)param).DialogResult = false;
            ((Window)param).Close();
        }
    }
}
