using Microsoft.Extensions.DependencyInjection;
using MyProductivityManager.Core.Context;
using MyProductivityManager.Core.Interfaces;
using MyProductivityManager.Core.Models;
using MyProductivityManager.Core.Models.ProjectTasks;
using MyProductivityManager.Core.Services;
using MyProductivityManager.Core.Views;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;

namespace MyProductivityManager.Core.ViewModels
{
    public class TasksViewModel : ViewModel
    {
        private readonly ApplicationDBContext _context;
        private readonly IServiceProvider _servicesProvider;
        private readonly IProjectsTasksRepository _projectsTasksRepository;
        private readonly IDialogService _dialogService;
        public ObservableCollection<Models.ProjectTasks.TaskStatus> StatusValues { get; set; }

        public ObservableCollection<Project> Projects { get; set; } = new ObservableCollection<Project>();
        private Project _selectedProject;
        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged();
                RefreshTasks();
            }
        }

        public IEnumerable<ProjectTask> TasksToDo => SelectedProject?.Tasks.Where(x => x.Status == Models.ProjectTasks.TaskStatus.ToDo) ?? Enumerable.Empty<ProjectTask>();
        public IEnumerable<ProjectTask> TasksInProgress => SelectedProject?.Tasks.Where(x => x.Status == Models.ProjectTasks.TaskStatus.InProgress) ?? Enumerable.Empty<ProjectTask>();
        public IEnumerable<ProjectTask> TasksDone => SelectedProject?.Tasks.Where(x => x.Status == Models.ProjectTasks.TaskStatus.Done) ?? Enumerable.Empty<ProjectTask>();

        public RelayCommand AddProjectCommand { get; set; }
        public RelayCommand EditProjectCommand { get; set; }
        public RelayCommand MoveLeftCommand { get; set; }
        public RelayCommand MoveRightCommand { get; set; }

        public TasksViewModel(ApplicationDBContext context, IServiceProvider servicesProvider, IProjectsTasksRepository projectsTasksRepository, IDialogService dialogService)
        {
            _context = context;
            _servicesProvider = servicesProvider;
            _dialogService = dialogService;
            _dialogService.RegisterDialog<ProjectDialogViewModel, ProjectDialogWindow>();
            _projectsTasksRepository = projectsTasksRepository;

            AddProjectCommand = new RelayCommand(obj => CreateNewProject(), obj => true);
            EditProjectCommand = new RelayCommand(obj =>  EditProject(), obj => SelectedProject != null);
            MoveLeftCommand = new RelayCommand(obj => MoveLeft(obj));
            MoveRightCommand = new RelayCommand(obj => MoveRight(obj));

            StatusValues = new ObservableCollection<Models.ProjectTasks.TaskStatus>(Enum.GetValues(typeof(Models.ProjectTasks.TaskStatus)).Cast<Models.ProjectTasks.TaskStatus>());
        }
        private void CreateNewProject()
        {
            OpenProjectDialogWindow();
        }
        private void EditProject()
        {
            OpenProjectDialogWindow(SelectedProject);
        }
        private void OpenProjectDialogWindow(Project project = null)
        {
            var vm = _servicesProvider.GetRequiredService<ProjectDialogViewModel>();
            vm.InitializeProject(project);
            var result = _dialogService.ShowDialog<ProjectDialogViewModel>(vm);
            if(result == true)
            {
                if (project == null)
                {
                    project = new Project();
                    Projects.Add(project);
                }
                else
                    project.DateUpdate = DateTime.Now;
                project.Name = vm.ProjectName;
                project.Description = vm.Description;
            }
        }
        private void MoveLeft(object param)
        {
            (param as ProjectTask).Status--;
            RefreshTasks();
        }
        private void MoveRight(object param)
        {
            (param as ProjectTask).Status++;
            RefreshTasks();
        }
        private void RefreshTasks()
        {
            OnPropertyChanged(nameof(TasksToDo));
            OnPropertyChanged(nameof(TasksInProgress));
            OnPropertyChanged(nameof(TasksDone));
        }
    }
}