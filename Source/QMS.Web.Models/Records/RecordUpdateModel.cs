namespace QMS.Web.Models.Records
{
    using QMS.Models;
    using Infrastructure.Mappings;
    using RecordFiles;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections;
    using Notes;
    using System.Collections.Generic;

    public class RecordUpdateModel : IMapFrom<Record>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? FinishingDate { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }

        [Required]
        public RecordStatus Status { get; set; }

        public int AreaId { get; set; }

        public ICollection<NoteDetailsModel> Notes { get; set; }
    }
}
