namespace QMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Document
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        public string FilePath { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
