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
        public async Task<Guid> Add([FromBody] AuthorDTO author)
        {
            var model = _mapper.Map<Author>(author);
            var newAuthor = await _authorProvider.Add(model);
            return newAuthor.Id;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Author>> Get()
        {
            return await _authorProvider.Get();
        }

        [HttpGet("GetAllPagination")]
        public async Task<IEnumerable<Author>> Get(int page, int pagesize, string filter, string sortPole)
        {
            return await _authorProvider.Get(page, pagesize, filter, sortPole);
        }

        [HttpPost("update/{id}")]
        public async Task<Guid> Update(Guid id, [FromBody] AuthorDTO author)
        {
            var model = _mapper.Map<Author>(author);
            var updateAuthor = await _authorProvider.Update(id, model);
            return updateAuthor.Id;
        }

        [HttpDelete("Delete")]
        public async Task<bool> Delete(Guid id)
        {
            return await _authorProvider.Delete(id);
        }

        [HttpGet("Get")]
        public async Task<Author> Get(Guid id)
        {
            return await _authorProvider.Get(id);
        }
    }
}
