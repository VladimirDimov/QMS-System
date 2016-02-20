using System;
using System.ComponentModel.DataAnnotations;

namespace QMS.Models
{
    public class Note
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; }

        public int RecordId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual Record Record { get; set; }
    }
}