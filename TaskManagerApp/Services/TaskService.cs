using TaskManagerApp.DTOs.TaskDTOs;
using TaskStatus = TaskManagerApp.Models.TaskStatus;

namespace TaskManagerApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync(string UserId)
        {
            var tasks = await _unitOfWork.GetRepository<Models.Task>().GetAllAsync();

            return tasks.Select(task => new TaskDto()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = (int)task.Status,
                Priority = (int)task.Priority,
                Date = task.Date
            });
        }
        public async Task<ServiceResult<IEnumerable<TaskDto>>> GetTasksByListIdAsync(string ListId, string UserId)
        {
            var list = await _unitOfWork.GetRepository<List>().GetByIdAsync(ListId);
            if (list is null)
            {
                return new ServiceResult<IEnumerable<TaskDto>>
                {
                    Success = false,
                    Message = "List not found."
                };
            }
            if (list.UserId != UserId)
            {
                return new ServiceResult<IEnumerable<TaskDto>>
                {
                    Success = false,
                    Message = "You are not authorized to access this list."
                };
            }
            var tasks = await _unitOfWork.GetRepository<Models.Task>()
                .GetAllAsync();
            var listTasks = tasks.Where(t => t.ListId == ListId);
            return new ServiceResult<IEnumerable<TaskDto>>()
            {
                Success = true,
                Data = listTasks.Select(task => new TaskDto()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = (int)task.Status,
                    Priority = (int)task.Priority,
                    Date = task.Date
                })
            };
        }

        public async Task<ServiceResult<TaskDto>> GetTaskByIdAsync(string Id, string UserId)
        {
            var task = await _unitOfWork.GetRepository<Models.Task>().GetByIdAsync(Id);
            if (task is null || task.List.UserId != UserId)
            {
                return new ServiceResult<TaskDto>
                {
                    Success = false,
                    Message = "Task not found."
                };
            }
            return new ServiceResult<TaskDto>()
            {
                Success = true,
                Data = new TaskDto()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = (int)task.Status,
                    Priority = (int)task.Priority,
                    Date = task.Date
                }
            };
        }

        public async Task<ServiceResult<TaskDto>> CreateTaskAsync(CreateTaskDto dto, string UserId)
        {
            var list = await _unitOfWork.GetRepository<List>().GetByIdAsync(dto.ListId);
            if (list is null || list.UserId != UserId)
            {
                return new ServiceResult<TaskDto>
                {
                    Success = false,
                    Message = "List not found."
                };
            }
            
            var task = new Models.Task
            {
                Title = dto.Title,
                Description = dto.Description,
                Date = dto.Date,
                Status = (TaskStatus)dto.Status,
                Priority = (TaskPriority)dto.Priority,
                ListId = dto.ListId
            };
            await _unitOfWork.GetRepository<Models.Task>().AddAsync(task);
            var result = await _unitOfWork.SaveAsync();
            if (result <= 0)
            {
                return new ServiceResult<TaskDto>
                {
                    Success = false,
                    Message = "Failed to create task."
                };
            }
            return new ServiceResult<TaskDto>
            {
                Success = true,
                Data = new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = (int)task.Status,
                    Priority = (int)task.Priority,
                    Date = task.Date
                }
            };
        }

        public async Task<ServiceResult<TaskDto>> UpdateTaskAsync(UpdateTaskDto dto, string UserId)
        {
            var tasks = await _unitOfWork.GetRepository<Models.Task>().GetAllAsync();
            var task = tasks.FirstOrDefault(t=>t.Title==dto.Title);
            if (task is not null)
            {
                return new ServiceResult<TaskDto>()
                {
                    Success = false,
                    Message = "Task with this title already exists."
                };
            }

            task =  tasks.FirstOrDefault(t=>t.Id==dto.TaskId);
            if (task is null)
            {
                return new ServiceResult<TaskDto>
                {
                    Success = false,
                    Message = "Task not found."
                };
            }

            var list = await _unitOfWork.GetRepository<List>().GetByIdAsync(task.ListId);
            if ( list.UserId != UserId)
            {
                return new ServiceResult<TaskDto>
                {
                    Success = false,
                    Message = "Task not found."
                };
            }

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = (TaskStatus)dto.Status;
            task.Priority = (TaskPriority)dto.Priority;
            task.Date = dto.Date;

            _unitOfWork.GetRepository<Models.Task>().Update(task);
            var result = await _unitOfWork.SaveAsync();
            if (result <= 0)
            {
                return new ServiceResult<TaskDto>
                {
                    Success = false,
                    Message = "Failed to update task."
                };
            }
            return new ServiceResult<TaskDto>
            {
                Success = true,
                Data = new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = (int)task.Status,
                    Priority = (int)task.Priority,
                    Date = task.Date
                }
            };

        }

        public async Task<ServiceResult<TaskDto>> DeleteTaskAsync(string Id, string UserId)
        {
            var tasks = await _unitOfWork.GetRepository<Models.Task>().GetAllAsync();
            var task = tasks.FirstOrDefault(t => t.Id == Id);
            
            if (task is null)
            {
                return new ServiceResult<TaskDto>
                {
                    Success = false,
                    Message = "Task not found."
                };
            }
            var list = await _unitOfWork.GetRepository<List>().GetByIdAsync(task.ListId);
            if (list.UserId != UserId)
            {
                return new ServiceResult<TaskDto>
                {
                    Success = false,
                    Message = "Task not found."
                };
            }
            _unitOfWork.GetRepository<Models.Task>().Delete(task);
            var result = await _unitOfWork.SaveAsync();
            if (result <= 0)
            {
                return new ServiceResult<TaskDto>()
                {
                    Success = false ,
                    Message = "Failed to update task"
                };
            }
            return new ServiceResult<TaskDto>
            {
                Success = true,
                Message = "Task deleted successfully.",
            };
        }
    }
    
}
