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
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherProvider _publisherProvider;
        private readonly IMapper _mapper;

        public PublishersController(IPublisherProvider publisherProvider, IMapper mapper)
        {
            _publisherProvider = publisherProvider;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] PublisherDTO publisher)
        {
            var model = _mapper.Map<Publisher>(publisher);
            var newPublisher = await _publisherProvider.Add(model);
            return Ok(newPublisher.Id);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var publishers = await _publisherProvider.Get();
            return Ok(publishers);
        }

        [HttpGet("GetAllPagination")]
        public async Task<IActionResult> Get(int page, int pagesize, string filter, string sort)
        {
            var publisher = await _publisherProvider.Get(page, pagesize, filter, sort);
            return Ok(publisher);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PublisherDTO publisher)
        {
            var model = _mapper.Map<Publisher>(publisher);
            var updatePublisher = await _publisherProvider.Update(id, model);
            return Ok(updatePublisher.Id);
        }
        
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete (Guid id)
        {
            var delete = await _publisherProvider.Delete(id);
            return Ok(delete);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var publiser = await _publisherProvider.Get(id);
            return Ok(publiser);
        }
    }
}
