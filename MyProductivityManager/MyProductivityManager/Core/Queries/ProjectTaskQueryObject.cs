using MyProductivityManager.Core.Models.ProjectTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Queries
{
    public class ProjectTaskQueryObject
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime DueDate { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public string AssignedTo { get; set; } = string.Empty;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
