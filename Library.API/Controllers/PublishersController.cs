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
        public async Task<ApiResult<Guid>> Add([FromBody] PublisherDTO publisher)
        {
            var model = _mapper.Map<Publisher>(publisher);
            var newPublisher = await _publisherProvider.Add(model);
            return ApiResult.Success(newPublisher.Id);
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult<IEnumerable<Publisher>>> Get()
        {
            var publishers = await _publisherProvider.Get();
            return ApiResult.Success(publishers);
        }

        [HttpGet("GetAllPagination")]
        public async Task<ApiResult<IEnumerable<Publisher>>> Get(int page, int pagesize, string filter, string sort)
        {
            var publisher = await _publisherProvider.Get(page, pagesize, filter, sort);
            return ApiResult.Success(publisher);
        }

        [HttpPost("update/{id}")]
        public async Task<ApiResult<Guid>> Update(Guid id, [FromBody] PublisherDTO publisher)
        {
            var model = _mapper.Map<Publisher>(publisher);
            var updatePublisher = await _publisherProvider.Update(id, model);
            return ApiResult.Success(updatePublisher.Id);
        }
        
        [HttpDelete("Delete")]
        public async Task<ApiResult<bool>> Delete (Guid id)
        {
            var delete = await _publisherProvider.Delete(id);
            return ApiResult.Success(delete);
        }

        [HttpGet("Get")]
        public async Task<ApiResult<Publisher>> Get(Guid id)
        {
            var publiser = await _publisherProvider.Get(id);
            return ApiResult.Success(publiser);
        }
    }
}
