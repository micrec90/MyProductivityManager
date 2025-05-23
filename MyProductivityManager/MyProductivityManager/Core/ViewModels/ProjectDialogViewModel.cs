﻿using MyProductivityManager.Core.Models.ProjectTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyProductivityManager.Core.ViewModels
{
    public class ProjectDialogViewModel : ViewModel
    {
        private string _windowTitle = "New project";
        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                OnPropertyChanged();
            }
        }
        private string _header = "New project";
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        private string _projectName = "New project";
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
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
        public void InitializeProject(Project project = null!)
        {
            if(project != null)
            {
                WindowTitle = "Edit project";
                Header = "Edit project";
                ProjectName = project.Name;
                Description = project.Description;
            }
        }
        public RelayCommand SaveProjectCommand { get; set; }
        public RelayCommand CancelProjectCommand { get; set; }
        public ProjectDialogViewModel()
        {
            SaveProjectCommand = new RelayCommand(obj => { SaveProject(obj); }, obj => true);
            CancelProjectCommand = new RelayCommand(obj => { CancelProject(obj); }, obj => true);
        }
        private void SaveProject(object param)
        {
            ((Window) param).DialogResult = true;
            ((Window) param).Close();
        }
        private void CancelProject(object param)
        {
            ((Window) param).DialogResult = false;
            ((Window) param).Close();
        }
    }
}
