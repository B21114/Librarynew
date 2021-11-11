using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.DTO
{
    /// <summary>
    /// Класс автор книги.
    /// </summary>
    public class AuthorDTO
    {
        /// <summary>
        /// Имя автора.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Firstname { get; set; }

        /// <summary>
        /// Отчество автора.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Patronymic { get; set; }

        /// <summary>
        /// Фамилия автора.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Lastname { get; set; }

        /// <summary>
        /// Деятельность автора.
        /// </summary>
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Activity { get; set; }
    }
}
