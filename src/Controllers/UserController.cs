using Backend.src.DTO;
using Backend.src.Service.Impl;
using Backend.src.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.Controller
{
    public class UserController : BaseController
    {
        protected readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllAsync([FromQuery] GetAllOptions getAllOptions)
        {
            var UserList = await _userService.GetAllAsync(getAllOptions);
            return Ok(UserList);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserReadDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var User = await _userService.GetByIdAsync(id);
            return Ok(User);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, UserUpdateDto updateDto)
        {
            var isUpdated = await _userService.UpdateOneAsync(id, updateDto);
            return Ok(isUpdated);
        }
        // id:guid => type of guid
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            var isDeleted = await _userService.DeleteOneASync(id);
            System.Console.WriteLine(isDeleted);
            return Ok(isDeleted);
        }

        // register
        [HttpPost("register")]
        public async Task<ActionResult<UserReadDto>> RegisterUser([FromBody] UserCreateDto userCreateDto)
        {
            var user = await _userService.CreateOneAsync(userCreateDto);
            return Ok(user);
        }


        // login
        [HttpPost("signIn")]
        public async Task<ActionResult<string>> SignInUser([FromBody] UserSignInDto userSignInDto)
        {
            var token = await _userService.SignInAsync(userSignInDto);
            return Ok(token);
        }
    }
}