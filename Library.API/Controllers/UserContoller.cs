using AutoMapper;
using Library.API.DTO;
using Library.BL.Services.Interfaces;
using Library.BL.Services.Providers;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProvider _userProvider;
        private readonly IMapper _mapper;

        public UserController(IUserProvider userProvider, IMapper mapper)
        {
            _userProvider = userProvider;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<ApiResult<Guid>> Add([FromBody] User user)
        {
          //  var model = _mapper.Map<Publisher>(publisher);
            var newUser = await _userProvider.Add(user);
            return ApiResult.Success(newUser.Id);
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult<IEnumerable<User>>> Get()
        {
            var users = await _userProvider.Get();
            return ApiResult.Success(users);
        }

        [HttpGet("GetAllPagination")]
        public async Task<ApiResult<IEnumerable<User>>> Get(int page, int pagesize, string filter, string sort)
        {
            var user = await _userProvider.Get(page, pagesize, filter, sort);
            return ApiResult.Success(user);
        }

        [HttpPost("update/{id}")]
        public async Task<ApiResult<Guid>> Update(Guid id, [FromBody] User user)
        {
          //  var model = _mapper.Map<Publisher>(publisher);
            var updateUser = await _userProvider.Update(id, user);
            return ApiResult.Success(updateUser.Id);
        }
        
        [HttpDelete("Delete")]
        public async Task<ApiResult<bool>> Delete (Guid id)
        {
            var delete = await _userProvider.Delete(id);
            return ApiResult.Success(delete);
        }

        [HttpGet("Get")]
        public async Task<ApiResult<User>> Get(Guid id)
        {
            var user = await _userProvider.Get(id);
            return ApiResult.Success(user);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ApiResult<User>> Authenticate(User user)
        {
            var authuser = await _userProvider.Authenticate(user.Email, user.PasswordStr);
            if (authuser is null)
            {
                return (ApiResult<User>)ApiResult.Failed();
            }
            return ApiResult.Success(authuser);
        }
    }
}
