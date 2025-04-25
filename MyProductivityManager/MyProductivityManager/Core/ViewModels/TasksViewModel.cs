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
        public ObservableCollection<ProjectTaskStatus> StatusValues { get; set; }

        public ObservableCollection<Project> Projects { get; set; } = new ObservableCollection<Project>();
        private Project _selectedProject = null!;
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
        public IEnumerable<ProjectTask> TasksToDo => SelectedProject?.Tasks.Where(x => x.Status == Models.ProjectTasks.ProjectTaskStatus.ToDo) ?? Enumerable.Empty<ProjectTask>();
        public IEnumerable<ProjectTask> TasksInProgress => SelectedProject?.Tasks.Where(x => x.Status == Models.ProjectTasks.ProjectTaskStatus.InProgress) ?? Enumerable.Empty<ProjectTask>();
        public IEnumerable<ProjectTask> TasksDone => SelectedProject?.Tasks.Where(x => x.Status == Models.ProjectTasks.ProjectTaskStatus.Done) ?? Enumerable.Empty<ProjectTask>();

        public RelayCommand AddProjectCommand { get; set; }
        public RelayCommand EditProjectCommand { get; set; }
        public RelayCommand DeleteProjectCommand { get; set; }
        public RelayCommand AddTaskCommand { get; set; }
        public RelayCommand MoveLeftCommand { get; set; }
        public RelayCommand MoveRightCommand { get; set; }

        public TasksViewModel(ApplicationDBContext context, IServiceProvider servicesProvider, IProjectsTasksRepository projectsTasksRepository, IDialogService dialogService)
        {
            _context = context;
            _servicesProvider = servicesProvider;
            _dialogService = dialogService;
            _dialogService.RegisterDialog<ProjectDialogViewModel, ProjectDialogWindow>();
            _dialogService.RegisterDialog<TaskDialogViewModel, TaskDialogWindow>();
            _dialogService.RegisterDialog<YesNoDialogViewModel, YesNoDialogWindow>();
            _projectsTasksRepository = projectsTasksRepository;
            AddProjectCommand = new RelayCommand(obj => CreateNewProject(), obj => true);
            EditProjectCommand = new RelayCommand(obj =>  EditProject(), obj => SelectedProject != null);
            DeleteProjectCommand = new RelayCommand(obj => DeleteProject(obj), obj => SelectedProject != null || obj != null);
            AddTaskCommand = new RelayCommand(obj => AddTask(), obj => SelectedProject != null);
            MoveLeftCommand = new RelayCommand(obj => MoveLeft(obj));
            MoveRightCommand = new RelayCommand(obj => MoveRight(obj));

            StatusValues = new ObservableCollection<ProjectTaskStatus>(Enum.GetValues(typeof(Models.ProjectTasks.ProjectTaskStatus)).Cast<ProjectTaskStatus>());
        }
        private void CreateNewProject()
        {
            OpenProjectDialogWindow();
        }
        private void EditProject()
        {
            OpenProjectDialogWindow(SelectedProject);
        }
        private void DeleteProject(object param = null!)
        {
            var vm = _servicesProvider.GetRequiredService<YesNoDialogViewModel>();
            var result = _dialogService.ShowDialog<YesNoDialogViewModel>(vm);
            if (result == true)
            {
                if (param == null)
                    Projects.Remove(SelectedProject);
                else
                    Projects.Remove((Project)param);
            }
        }
        private void AddTask()
        {
            OpenTaskDialogWindow();
        }
        private void OpenProjectDialogWindow(Project project = null!)
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
        private void OpenTaskDialogWindow()
        {
            var vm = _servicesProvider.GetRequiredService<TaskDialogViewModel>();
            var result = _dialogService.ShowDialog<TaskDialogViewModel>(vm);
            if (result == true)
            {
                SelectedProject.Tasks.Add(new ProjectTask
                {
                    Title = vm.TaskName,
                    Description = vm.Description,
                    Status = vm.TaskStatus,
                    Priority = vm.TaskPriority
                });
                RefreshTasks();
            }
        }
        private void MoveLeft(object param)
        {
            ((ProjectTask) param).Status--;
            RefreshTasks();
        }
        private void MoveRight(object param)
        {
            ((ProjectTask) param).Status++;
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