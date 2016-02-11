namespace QMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RecordFile
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public DateTime DateUpdated { get; set; }

        public int RecordId { get; set; }

        public Record Record { get; set; }
    }
}
