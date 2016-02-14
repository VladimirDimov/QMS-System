namespace QMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Note
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public int RecordId { get; set; }

        public virtual Record Record { get; set; }
    }
}