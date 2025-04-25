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
    public class TaskDialogViewModel : ViewModel
    {
        public ObservableCollection<ProjectTaskStatus> StatusValues { get; set; }

        private string _windowTitle = "New task";
        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                OnPropertyChanged();
            }
        }
        private string _header = "New task";
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        private string _taskName = "New task";
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged();
            }
        }
        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        private TaskPriority _taskPriority;
        public TaskPriority TaskPriority
        {
            get { return _taskPriority; }
            set
            {
                _taskPriority = value;
                OnPropertyChanged();
            }
        }
        private ProjectTaskStatus _taskStatus;
        public ProjectTaskStatus TaskStatus
        {
            get { return _taskStatus; }
            set
            {
                _taskStatus = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand SaveProjectCommand { get; set; }
        public RelayCommand CancelProjectCommand { get; set; }
        public TaskDialogViewModel()
        {
            StatusValues = new ObservableCollection<ProjectTaskStatus>(Enum.GetValues(typeof(ProjectTaskStatus)).Cast<ProjectTaskStatus>());

            SaveProjectCommand = new RelayCommand(obj => { SaveProject(obj); }, obj => true);
            CancelProjectCommand = new RelayCommand(obj => { CancelProject(obj); }, obj => true);
        }
        private void SaveProject(object param)
        {
            ((Window)param).DialogResult = true;
            ((Window)param).Close();
        }
        private void CancelProject(object param)
        {
            ((Window)param).DialogResult = false;
            ((Window)param).Close();
        }
    }
}
