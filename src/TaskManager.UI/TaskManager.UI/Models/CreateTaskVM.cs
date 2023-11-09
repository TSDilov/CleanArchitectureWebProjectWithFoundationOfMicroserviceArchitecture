namespace TaskManager.UI.Models
{
    public class CreateTaskVM
    {

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
