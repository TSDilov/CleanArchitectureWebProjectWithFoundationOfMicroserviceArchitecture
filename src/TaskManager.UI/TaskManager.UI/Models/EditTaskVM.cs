namespace TaskManager.UI.Models
{
    public class EditTaskVM
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
