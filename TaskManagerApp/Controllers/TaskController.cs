using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.DTOs.TaskDTOs;

namespace TaskManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly ITaskService _taskService;

        public TaskController(UserManager<ApplicationUser> userManager, ITaskService taskService)
        {
            _userManger = userManager;
            _taskService = taskService;
        }


        [HttpGet("get-all-tasks")]
        [Authorize]
        public async Task<IActionResult> GetAllTasksAsync()
        {
            var user = await _userManger.GetUserAsync(User);
            if (user is null)
                return Unauthorized("User Not Found");
            var tasks = await _taskService.GetAllTasksAsync(user.Id);

            return Ok(tasks);
        }

        [HttpGet("get-task-by-list-id")]
        [Authorize]
        public async Task<IActionResult> GetTasksByUserIdAsync(string ListId)
        {
            var user = await _userManger.GetUserAsync(User);
            if (user is null)
                return Unauthorized("User Not Found");

            var result = await _taskService.GetTasksByListIdAsync(ListId, user.Id);

            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }


        [HttpGet("get-task-by-id")]
        [Authorize]
        public async Task<IActionResult> GetTaskByIdAsync(string Id)
        {
            var user = await _userManger.GetUserAsync(User);
            if (user is null)
                return Unauthorized("User Not Found");

            var result = await _taskService.GetTaskByIdAsync(Id, user.Id);

            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpPost("create-task")]
        [Authorize]
        public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManger.GetUserAsync(User);
            if (user is null)
                return Unauthorized("User Not Found");

            var result = await _taskService.CreateTaskAsync(dto, user.Id);

            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }


        [HttpPut("update-task")]
        [Authorize]
        public async Task<IActionResult> UpdateTaskAsync([FromBody] UpdateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManger.GetUserAsync(User);
            if (user is null)
                return Unauthorized("User Not Found");

            var result = await _taskService.UpdateTaskAsync(dto, user.Id);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Data);


        }


        [HttpDelete("delete-task")]
        [Authorize]
        public async Task<IActionResult> DeleteTaskAsync(string Id)
        {
            var user = await _userManger.GetUserAsync(User);
            if (user is null)

                return Unauthorized("User Not Found");
            var result = await _taskService.DeleteTaskAsync(Id, user.Id);
            if (!result.Success)

                return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}
