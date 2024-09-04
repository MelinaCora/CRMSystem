namespace CRMSystem.Models
{
    public class Interactions
    {
        public Guid InteractionID { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public Guid ProjectID { get; set; }        // Foreign Key
        public Projects Project { get; set; }

        public int InteractionType { get; set; } // Foreign Key
        public InteractionTypes Interactionstype { get; set; }
    }
}
