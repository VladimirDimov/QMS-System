namespace QMS.Models
{
    using System;

    public class Document
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string FilePath { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
