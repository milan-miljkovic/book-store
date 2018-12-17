using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Cart : BaseModel
    {
        [Key]
        public int RecordId { get; set; }
        [Required]
        public string CartId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public virtual Book Book { get; set; }
    }
}
