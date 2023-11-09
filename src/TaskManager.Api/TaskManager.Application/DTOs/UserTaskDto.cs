using TaskManager.Application.DTOs.Common;

namespace TaskManager.Application.DTOs
{
    public class UserTaskDto : BaseDto, IUserTaskDto
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
