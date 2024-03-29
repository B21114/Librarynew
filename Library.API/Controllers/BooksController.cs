﻿using AutoMapper;
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
        public async Task<IActionResult> Add([FromBody] BookDTO book)
        {
            var model = _mapper.Map<Book>(book);
            var newBook = await _bookProvider.Add(model);
            return Ok(newBook.Id);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {

            var books = await _bookProvider.Get();
            return Ok(books);
        }

        [HttpGet("GetAllFilterSortPagination")]
        public async Task<IActionResult> Get(string filter, string sortPole, int pageNumber = 1, int pageSize = 10)
        {
            var books = await _bookProvider.Get(pageNumber, pageSize, filter, sortPole);
            return Ok(books);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookDTO book)
        {
            var model = _mapper.Map<Book>(book);
            var updateBook = await _bookProvider.Add(model);
            return Ok(updateBook.Id);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = await _bookProvider.Delete(id);
            return Ok(delete);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var book = await _bookProvider.Get(id);
            return Ok(book);
        }
    }
}
