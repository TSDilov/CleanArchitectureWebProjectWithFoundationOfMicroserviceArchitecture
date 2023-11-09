using TaskManager.Infrastructure.Dtos.Common;

namespace TaskManager.Infrastructure.Dtos
{
    public class UserTaskDto : BaseDto, IUserTaskDto
    {
        public string User { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
