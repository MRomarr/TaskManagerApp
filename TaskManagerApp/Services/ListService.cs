using TaskManagerApp.DTOs.ListDTOs;
using TaskManagerApp.DTOs.TaskDTOs;

namespace TaskManagerApp.Services
{
    public class ListService : IListService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ListDto>> GetAllListsAsync(string UserId)
        {
            var result = await _unitOfWork.GetRepository<List>().GetAllAsync();
            return result.Where(l => l.UserId == UserId)
                .Select(l => new ListDto
                {
                    ListId = l.Id,
                    Name = l.Name
                });
        }

        public async Task<ServiceResult<ListDto>> CreateListAsync(CreateListDto dto,string userId)
        {
            var lists = await GetAllListsAsync(userId);
            if (lists.Any(l => l.Name == dto.Name))
            {
                return new ServiceResult<ListDto>
                {
                    Success = false,
                    Message = "List name already exists."
                };
            }

            var list = new List
            {
                Name = dto.Name,
                UserId = userId
            };

            await _unitOfWork.GetRepository<List>().AddAsync(list);
            var result = await _unitOfWork.SaveAsync();
            if (result <= 0)
            {
                return new ServiceResult<ListDto>
                {
                    Success = false,
                    Message = "Failed to create list."
                };
            }

            return new ServiceResult<ListDto>
            {
                Success = true,
                Data = new ListDto
                {
                    ListId = list.Id,
                    Name = list.Name
                }
            };
        }

        public async Task<ServiceResult<ListDto>> UpdateListAsync(UpdateListDto dto, string UserId)
        {
            var lists = await GetAllListsAsync(UserId);
            if (!lists.Any(l => l.ListId == dto.ListId))
            {
                return new ServiceResult<ListDto>
                {
                    Success = false,
                    Message = "List not found."
                };
            }

            if (lists.Any(l => l.Name == dto.NewName && l.ListId != dto.ListId))
            {
                return new ServiceResult<ListDto>
                {
                    Success = false,
                    Message = "List name already exists."
                };
            }

            var list = await _unitOfWork.GetRepository<List>().GetByIdAsync(dto.ListId);
            list.Name = dto.NewName;
            _unitOfWork.GetRepository<List>().Update(list);

            var result = await _unitOfWork.SaveAsync();
            if (result <= 0)
            {
                return new ServiceResult<ListDto>
                {
                    Success = false,
                    Message = "Failed to update list."
                };
            }

            return new ServiceResult<ListDto>
            {
                Success = true,
                Data = new ListDto
                {
                    ListId = list.Id,
                    Name = list.Name
                }

            };
        }

        public async Task<ServiceResult<ListDto>> DeleteListAsync(string ListId, string UserId)
        {
            var lists = await GetAllListsAsync(UserId);
            if (!lists.Any(l => l.ListId == ListId))
            {
                return new ServiceResult<ListDto>
                {
                    Success = false,
                    Message = "List not found."
                };
            }
            var list = await _unitOfWork.GetRepository<List>().GetByIdAsync(ListId);
            _unitOfWork.GetRepository<List>().Delete(list);
            var result = await _unitOfWork.SaveAsync();
            if (result <= 0)
            {
                return new ServiceResult<ListDto>
                {
                    Success = false,
                    Message = "Failed to delete list."
                };
            }
            return new ServiceResult<ListDto>
            {
                Success = true,
                Message = "List deleted successfully."
            };
        }
    }
}
