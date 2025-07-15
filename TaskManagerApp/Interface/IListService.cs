using TaskManagerApp.DTOs.ListDTOs;
using TaskManagerApp.DTOs.TaskDTOs;

namespace TaskManagerApp.Interface
{
    public interface IListService
    {
        Task<IEnumerable<ListDto>> GetAllListsAsync(string UserId);
        Task<ServiceResult<ListDto>> CreateListAsync(CreateListDto dto,string UserId);
        Task<ServiceResult<ListDto>> UpdateListAsync(UpdateListDto dto,string UserId);
        Task<ServiceResult<ListDto>> DeleteListAsync(string ListId,string UserId);

        



    }
}
