namespace TaskManager.UI.Models
{
    public class TaskListVM : PagingVM
    {
        public IEnumerable<TaskVM> Tasks { get; set; }
    }
}
