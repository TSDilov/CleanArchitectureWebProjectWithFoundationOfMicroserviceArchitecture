namespace TaskManager.UI.Models
{
    public class TaskVM
    {
        public int Id { get; set; }

        public string User { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public DateTime StartDateTimeLocal => StartDateTime.ToLocalTime();

        public DateTime EndDateTimeLocal => EndDateTime.ToLocalTime();

        public DateTime Date => StartDateTimeLocal.Date;

        public TimeOnly StartTime => new TimeOnly(StartDateTimeLocal.Hour, StartDateTimeLocal.Minute, StartDateTimeLocal.Second);

        public TimeOnly EndTime => new TimeOnly(EndDateTimeLocal.Hour, EndDateTimeLocal.Minute, EndDateTimeLocal.Second);

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
