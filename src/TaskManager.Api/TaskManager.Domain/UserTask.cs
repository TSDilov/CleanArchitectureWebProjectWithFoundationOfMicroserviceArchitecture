namespace TaskManager.Domain
{
    public class UserTask
    {
        public int Id { get; set; }

        public string User { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
