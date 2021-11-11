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
    public class BooksController : ControllerBase
    {
        private readonly IBookProvider _bookProvider;
        private readonly IMapper _mapper;

        public BooksController(IBookProvider bookProvider, IMapper mapper)
        {
            _bookProvider = bookProvider;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<Guid> Add([FromBody] BookDTO book)
        {
            var model = _mapper.Map<Book>(book);
            var newBook = await _bookProvider.Add(model);
            return newBook.Id;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Book>> Get()
        {
            return await _bookProvider.Get();
        }

        [HttpGet("GetAllPagination")]
        public async Task<IEnumerable<Book>> Get(int page, int pagesize, string filter, string sortPole)
        {
            return await _bookProvider.Get(page, pagesize, filter, sortPole);
        }

        [HttpPost("update/{id}")]
        public async Task<Guid> Update(Guid id, [FromBody] BookDTO book)
        {
            var model = _mapper.Map<Book>(book);
            var updateBook = await _bookProvider.Add(model);
            return updateBook.Id;
        }

        [HttpDelete("Delete")]
        public async Task<bool> Delete(Guid id)
        {
            return await _bookProvider.Delete(id);
        }

        [HttpGet("Get")]
        public async Task<Book> Get(Guid id)
        {
            return await _bookProvider.Get(id);
        }
    }
}
