namespace TaskManager.UI.Models
{
    public class PagingVM
    {
        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumer => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.TasksCount / 12);

        public int TasksCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
