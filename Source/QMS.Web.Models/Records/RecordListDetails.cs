namespace QMS.Web.Models.Records
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using System;
    using System.Collections.Generic;

    public class RecordListModel : IMapFrom<Record>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Document Document { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? FinishingDate { get; set; }

        public DateTime StatusDate { get; set; }

        public RecordStatus Status { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
