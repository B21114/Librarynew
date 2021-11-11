using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.DTO
{
    /// <summary>
    /// Класс книги.
    /// </summary>
    public class BookDTO
    {

        /// <summary>
        /// Наименование книги.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        /// <summary>
        /// Количество страниц книги.
        /// </summary>
        [Required]
        [Range(1, 2000000000)]
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Авторы книги.
        /// </summary>
        [Required]
        public List<Guid> AuthorsId { get; set; }

        /// <summary>
        /// Издатель книги.
        /// </summary>
        [Required]
        public Guid PublisherId { get; set; }
    }
}
