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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IMapper _mapper;
        public AuthorsController(IAuthorProvider authorProvider, IMapper mapper)
        {
            _authorProvider = authorProvider;
            _mapper = mapper;
        }

        //[HttpPost("Add")]
        //public async Task<IActionResult> Add([FromBody] AuthorDTO author)
        //{
        //    var model = _mapper.Map<Author>(author);
        //    var newAuthor = await _authorProvider.Add(model);
        //    return Ok(newAuthor.Id);
        //}

        [HttpPost("Add")]
        public async Task<ApiResult<Guid>> Add([FromBody] AuthorDTO author)
        {
            var model = _mapper.Map<Author>(author);
            var newAuthor = await _authorProvider.Add(model);

            return ApiResult.Success(newAuthor.Id);
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult<IEnumerable<Author>>> Get()
        {
            var authors = await _authorProvider.Get();
            return ApiResult.Success(authors);
        }

        [HttpGet("GetAllFilterSortPagination")]
        public async Task<ApiResult<IEnumerable<Author>>> Get(int page, int pagesize, string filter, string sortPole)
        {
            var authors = await _authorProvider.Get(page, pagesize, filter, sortPole);
            return ApiResult.Success(authors);
        }

        [HttpPost("update/{id}")]
        public async Task<ApiResult<Guid>> Update(Guid id, [FromBody] AuthorDTO author)
        {
            var model = _mapper.Map<Author>(author);
            var updateAuthor = await _authorProvider.Update(id, model);
            return ApiResult.Success(updateAuthor.Id);
        }

        [HttpDelete("Delete")]
        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var authorDelete = await _authorProvider.Delete(id);
            return ApiResult.Success(authorDelete);
        }

        [HttpGet("Get")]
        public async Task<ApiResult<Author>> Get(Guid id)
        {
            var author = await _authorProvider.Get(id);
            return ApiResult.Success(author);
        }
    }
}
