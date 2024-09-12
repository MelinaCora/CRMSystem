namespace CRMSystem.Models
{
    public class Tasks
    {
        public Guid TaskID { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime UpdateDate  { get; set; }

        public Guid ProjectID { get; set; }        // Foreign Key
        public Projects Project { get; set; }

        public int AssignedTo { get; set; }        // Foreign Key
        public Users AssignedUser { get; set; }

        public int StatusId { get; set; }          // Foreign Key
        public TaskStatus Status { get; set; }
    }
}
