namespace QMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RecordFile
    {
        [Key]
        public int Id { get; set; }

        public string Path { get; set; }

        public DateTime DateUpdated { get; set; }

        public int RecordId { get; set; }

        public virtual Record Record { get; set; }
    }
}
