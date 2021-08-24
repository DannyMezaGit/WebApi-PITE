using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AlumnDTO
    {
        public long AlumnId { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "El nombre es demasiado largo")]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        [StringLength(70, ErrorMessage = "El comentario es demasiado largo")]
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}