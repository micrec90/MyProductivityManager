using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Models.ProjectTasks
{
    public class ProjectTask
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime DueDate { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        public string AssignedTo { get; set; } = string.Empty;
    }
}
