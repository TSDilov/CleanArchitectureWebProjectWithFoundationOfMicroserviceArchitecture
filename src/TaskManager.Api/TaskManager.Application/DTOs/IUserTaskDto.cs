namespace TaskManager.Application.DTOs
{
    public interface IUserTaskDto
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
