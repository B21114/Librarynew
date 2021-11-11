using Library.BL.Services.Interfaces;
using Library.DL.Services.Interfaces;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL.Services.Providers
{
    public class PublisherProvider : IPublisherProvider
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherProvider(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<Publisher> Add(Publisher publisher)
        {
            return await _publisherRepository.Add(publisher);
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var publisher = await _publisherRepository.Get(id);
                await _publisherRepository.Delete(publisher);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Publisher>> Get()
        {
            return await _publisherRepository.Get();
        }

        public async Task<IEnumerable<Publisher>> Get(int page, int pageSize, string filter, string sort)
        {
            if (page < 1 || pageSize < 1)
            {
                return null;
            }
          //  if (pole == null)
         //   { }
                

            return await _publisherRepository.Get(page, pageSize, filter, sort); ;
        }

        public async Task<Publisher> Get(Guid id)
        {
            return await _publisherRepository.Get(id);
        }

        public async Task<Publisher> Update(Guid id, Publisher publisher)
        {
            var publisherDB = await _publisherRepository.Get(id);
            if (publisherDB != null)
            {
                publisherDB.Name = publisher.Name;
                publisherDB.City = publisher.City;
                return await _publisherRepository.Update(id, publisherDB);
            }
            return await _publisherRepository.Add(new Publisher());
            
        }
    }
}
