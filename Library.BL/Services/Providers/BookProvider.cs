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
    public class BookProvider : IBookProvider
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;

        public BookProvider(IBookRepository bookRepository,
            IAuthorRepository authorRepository, IPublisherRepository publisherRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
        }

        public async Task<Book> Add(Book book)
        {
            return await _bookRepository.Add(book);
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var book = await _bookRepository.Get(id);
                await _bookRepository.Delete(book);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await _bookRepository.Get();
        }

        public async Task<IEnumerable<Book>> Get(int page, int pageSize, string filter, string sortPole)
        {
            if (page < 1 || pageSize < 1)
            {
                return null;
            }

            return await _bookRepository.Get(page, pageSize, filter, sortPole); ;
        }

        public async Task<Book> Get(Guid id)
        {
            return await _bookRepository.Get(id);
        }

        public async Task<Book> Update(Guid id, Book book)
        {
            var bookDB = await _bookRepository.Get(id);
            if (bookDB != null)
            {
                bookDB.Name = book.Name;
                bookDB.NumberOfPages = book.NumberOfPages;
                bookDB.Authors = book.Authors;
                bookDB.Publisher = book.Publisher;

                return await _bookRepository.Update(id, bookDB);
            }
            return await _bookRepository.Add(new Book());
        }
    }
}
