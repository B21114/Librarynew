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
    public class AuthorProvider : IAuthorProvider
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorProvider(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Add(Author author)
        {

            return await _authorRepository.Add(author);
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var author = await _authorRepository.Get(id);
                await _authorRepository.Delete(author);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Author>> Get()
        {
            return await _authorRepository.Get();
        }

        public async Task<IEnumerable<Author>> Get(int page, int pageSize, string filter, string sortPole)
        {
            if (page < 1 || pageSize < 1)
            {
                return null;
            }

            var author = await _authorRepository.Get(page, pageSize, filter, sortPole);

            return author;
        }

        public async Task<Author> Get(Guid id)
        {
            return await _authorRepository.Get(id);
        }

        public async Task<Author> Update(Guid id, Author author)
        {
            var userDB = await _authorRepository.Get(id);
            if (userDB != null)
            {
                userDB.Lastname = author.Lastname;
                userDB.Firstname = author.Firstname;
                userDB.Patronymic = author.Patronymic;
                userDB.Activity = author.Activity;
                return await _authorRepository.Update(id, userDB);
            }
            return await _authorRepository.Add(new Author());
        }
    }
}
