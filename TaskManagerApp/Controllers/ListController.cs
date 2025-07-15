using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.DTOs.ListDTOs;

namespace TaskManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;
        private readonly UserManager<ApplicationUser> _userManger;

        public ListController(IListService listService, UserManager<ApplicationUser> userManger)
        {
            _listService = listService;
            _userManger = userManger;
        }
        

        [HttpGet("get-all-Lists")]
        [Authorize]
        public async Task<IActionResult> GetAllLists()
        {
            var user = await _userManger.GetUserAsync(User);
            if (user is null)
                return Unauthorized("User Not Found");
            var lists = await _listService.GetAllListsAsync(user.Id);
            return Ok(lists);
        }


        [HttpPost("create-list")]
        [Authorize]
        public async Task<IActionResult> CreateList([FromBody] CreateListDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManger.GetUserAsync(User);
            if (user is null)
                return Unauthorized("User Not Found");

            var result = await _listService.CreateListAsync(dto, user.Id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }


        [HttpPut("update-list")]
        [Authorize]
        public async Task<IActionResult> UpdateList([FromBody] UpdateListDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManger.GetUserAsync(User);
            if (user is null)
                return Unauthorized("User Not Found");

            var result = await _listService.UpdateListAsync(dto, user.Id);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }


        [HttpDelete("delete-list")]
        [Authorize]
        public async Task<IActionResult> DeleteList([FromQuery]string listId)
        {
            var user = await _userManger.GetUserAsync(User);
            if (user is null)
                return Unauthorized("User Not Found");

            var result = await _listService.DeleteListAsync(listId, user.Id);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}
