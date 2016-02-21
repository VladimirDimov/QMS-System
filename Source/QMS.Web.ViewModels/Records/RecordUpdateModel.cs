namespace QMS.Web.ViewModels.Records
{
    using Infrastructure.Mappings;
    using Notes;
    using QMS.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class RecordUpdateViewModel : IMapFrom<Record>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [AllowHtml]
        public string Title { get; set; }

        [MaxLength(200)]
        [UIHint("TextArea")]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [Required]
        [DisplayName("Created on")]
        public DateTime DateCreated { get; set; }

        [DisplayName("End date")]
        public DateTime? FinishingDate { get; set; }

        [Required]
        [DisplayName("Status date")]
        public DateTime StatusDate { get; set; }

        [Required]
        public RecordStatus Status { get; set; }

        public int AreaId { get; set; }

        public ICollection<NoteDetailsViewModel> Notes { get; set; }
    }
}
