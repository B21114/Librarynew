using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.DTO
{
    /// <summary>
    /// Класс издателя.
    /// </summary>
    public class PublisherDTO
    {
        /// <summary>
        /// Наименование издателя.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        /// <summary>
        /// Город издателя.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }
    }
}
