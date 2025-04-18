namespace TaskManagementSystem.Models
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskDueDate { get; set; }
        public string TaskStatus { get; set; }
        public string TaskRemarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedById { get; set; } = 1;
        public string LastUpdatedBy { get; set; }
        public int? LastUpdatedById { get; set; } = 1;
    }

}
