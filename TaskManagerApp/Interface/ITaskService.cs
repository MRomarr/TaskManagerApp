using TaskManagerApp.DTOs.TaskDTOs;

namespace TaskManagerApp.Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync(string UserId);
        Task<ServiceResult<IEnumerable<TaskDto>>> GetTasksByListIdAsync(string ListId,string UserId);
        Task<ServiceResult<TaskDto>> GetTaskByIdAsync(string Id, string UserId);
        Task<ServiceResult<TaskDto>> CreateTaskAsync(CreateTaskDto dto, string UserId);
        Task<ServiceResult<TaskDto>> UpdateTaskAsync(UpdateTaskDto dto, string UserId);
        Task<ServiceResult<TaskDto>> DeleteTaskAsync(string Id, string UserId);
        

    }
}
