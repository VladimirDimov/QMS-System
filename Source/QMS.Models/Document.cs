namespace QMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        [Required]
        public string FilePath { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
