namespace CRMSystem.Models
{
    public class Tasks
    {
        public Guid TaskID { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate  { get; set; }

        public Guid ProjectID { get; set; }
        public Projects Project { get; set; }

        public int AssignedTo { get; set; }
        public Users AssignedUser { get; set; }

        public int Status { get; set; }       
        public TaskStatus TaskStatus { get; set; }
    }
}
