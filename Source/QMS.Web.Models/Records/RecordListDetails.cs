namespace QMS.Web.Models.Records
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using Areas;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class RecordListModel : IMapFrom<Record>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Document Document { get; set; }

        [DisplayName("Created on")]
        public DateTime DateCreated { get; set; }

        [DisplayName("End date")]
        public DateTime? FinishingDate { get; set; }

        [DisplayName("Status date")]
        public DateTime StatusDate { get; set; }

        public RecordStatus Status { get; set; }

        public ICollection<Note> Notes { get; set; }

        public int? RecordFileId { get; set; }

        public virtual AreaShortViewModel Area { get; set; }
    }
}
