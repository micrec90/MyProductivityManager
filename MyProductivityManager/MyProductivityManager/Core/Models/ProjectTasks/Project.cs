using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Models.ProjectTasks
{
    public class Project : ObservableObject
    {
        public int Id { get; set; }
        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime DateUpdate { get; set; } = DateTime.Now;
        public ObservableCollection<ProjectTask> Tasks { get; set; } = new ObservableCollection<ProjectTask>();
    }
}
