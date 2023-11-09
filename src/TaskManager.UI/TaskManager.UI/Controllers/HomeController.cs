using Microsoft.AspNetCore.Mvc;
using TaskManager.UI.Models;
using TaskManager.Infrastructure.Contracts;
using AutoMapper;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ITaskManagerService taskManagerService;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger,
            ITaskManagerService taskManagerService,
            IMapper mapper)
        {
            this.logger = logger;
            this.taskManagerService = taskManagerService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 1)
        {
            var tasks = await this.taskManagerService.GetAllTasks(id, 12, cancellationToken);
            var viewModel = new TaskListVM
            {
                PageNumber = id,
                Tasks = this.mapper.Map<List<TaskVM>>(tasks),
                TasksCount = await this.taskManagerService.GetCount(cancellationToken),
                ItemsPerPage = 12
            };

            return View(viewModel);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTaskVM task, CancellationToken cancellationToken)
        {
            try
            {
                var dto = new CreateUserTaskDto()
                {
                    Description = task.Description,
                    StartDateTime = task.Date + task.StartTime,
                    EndDateTime = task.Date + task.EndTime,
                    Subject = task.Subject,
                };

                await taskManagerService.Create(dto, cancellationToken);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return LocalRedirect("/");
        }

        public async Task<ActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var task = await taskManagerService.GetById(id, cancellationToken);
            var startDateTime = task.StartDateTime.ToLocalTime();
            var endDateTime = task.EndDateTime.ToLocalTime();

            var vm = new EditTaskVM()
            {
                Date = startDateTime.Date,
                Description = task.Description,
                Id = id,
                Subject = task.Subject,
                StartTime = startDateTime.TimeOfDay,
                EndTime = endDateTime.TimeOfDay
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditTaskVM task, CancellationToken cancellationToken)
        {
            try
            {
                var dto = new EditUserTaskDto()
                {
                    Id = task.Id,
                    Description = task.Description,
                    StartDateTime = (task.Date + task.StartTime).ToUniversalTime(),
                    EndDateTime = (task.Date + task.EndTime).ToUniversalTime(),
                    Subject = task.Subject,
                };

                await taskManagerService.Edit(dto, cancellationToken);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(task);
            }

            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await taskManagerService.Delete(id, cancellationToken);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return Redirect("/");
        }
    }
}