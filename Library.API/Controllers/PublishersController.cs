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
        public async Task<Guid> Add([FromBody] PublisherDTO publisher)
        {
            var model = _mapper.Map<Publisher>(publisher);
            var newPublisher = await _publisherProvider.Add(model);
            return newPublisher.Id;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Publisher>> Get()
        {
            return await _publisherProvider.Get();
        }

        [HttpGet("GetAllPagination")]
        public async Task<IEnumerable<Publisher>> Get(int page, int pagesize, string filter, string sort)
        {
            return await _publisherProvider.Get(page, pagesize, filter, sort);
        }

        [HttpPost("update/{id}")]
        public async Task<Guid> Update(Guid id, [FromBody] PublisherDTO publisher)
        {
            var model = _mapper.Map<Publisher>(publisher);
            var updatePublisher = await _publisherProvider.Update(id, model);
            return updatePublisher.Id;
        }
        
        [HttpDelete("Delete")]
        public async Task<bool> Delete (Guid id)
        {
            return await _publisherProvider.Delete(id);
        }

        [HttpGet("Get")]
        public async Task<Publisher> Get(Guid id)
        {
            return await _publisherProvider.Get(id);
        }
    }
}
