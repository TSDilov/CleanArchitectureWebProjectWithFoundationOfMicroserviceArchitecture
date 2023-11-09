namespace TaskManager.Infrastructure.Dtos
{
    public class EditUserTaskDto
    {
        public int Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
