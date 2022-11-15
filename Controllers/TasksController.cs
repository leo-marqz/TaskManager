using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IUserService userService;

        public TasksController(ApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<List<Entities.Task>> GetAll()
        {
            var tasks = await context.Tasks.ToListAsync();
            return tasks;
        }

        [HttpPost()]
        public async Task<ActionResult<Entities.Task>> Create([FromBody] string title)
        {
            var userId = userService.GetUserId();
            var existTasks = await context.Tasks.AnyAsync(task => task.UserCreatedTaskId == userId);
            var order = 0;
            if (existTasks)
            {
                order = await context.Tasks.Where(task => task.UserCreatedTaskId == userId)
                    .Select(task => task.Order)
                    .MaxAsync();
            }

            var task = new Entities.Task
            {
                Title = title,
                UserCreatedTaskId = userId,
                Order = order + 1,
                CreatedAt = DateTime.UtcNow
            };
            context.Add(task);
            await context.SaveChangesAsync();
            return Ok("Tarea guardada exitosamente!!");
        }
    }
}
