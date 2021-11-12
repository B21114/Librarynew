using AutoMapper;
using Library.API.DTO;
using Library.BL.Services.Interfaces;
using Library.BL.Services.Providers;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AuthorDTO author)
        {
            var model = _mapper.Map<Author>(author);
            var newAuthor = await _authorProvider.Add(model);
            return Ok(newAuthor.Id);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var authors = await _authorProvider.Get();
            return Ok(authors);
        }

        [HttpGet("GetAllFilterSortPagination")]
        public async Task<IActionResult> Get(int page, int pagesize, string filter, string sortPole)
        {
            var authors = await _authorProvider.Get(page, pagesize, filter, sortPole);
            return Ok(authors);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AuthorDTO author)
        {
            var model = _mapper.Map<Author>(author);
            var updateAuthor = await _authorProvider.Update(id, model);
            return Ok(updateAuthor.Id);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var authorDelete = await _authorProvider.Delete(id);
            return Ok(authorDelete);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var author = await _authorProvider.Get(id);
            return Ok(author);
        }
    }
}
