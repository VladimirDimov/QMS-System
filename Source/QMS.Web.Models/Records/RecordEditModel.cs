namespace QMS.Web.Models.Records
{
    using QMS.Models;
    using Infrastructure.Mappings;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    class RecordEditModel : IMapFrom<Record>
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(200)]
        [UIHint("TextArea")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Document")]
        public int DocumentId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? FinishingDate { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }

        [Required]
        public RecordStatus Status { get; set; }
    }
}
