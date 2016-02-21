namespace QMS.Web.Models.Records
{
    using QMS.Models;
    using QMS.Web.Models.RecordFiles;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class RecordCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        [AllowHtml]
        public string Title { get; set; }

        [MaxLength(200)]
        [AllowHtml]
        [UIHint("TextArea")]
        public string Description { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [DisplayName("End date")]
        public DateTime? FinishingDate { get; set; }

        [Required]
        [DisplayName("Status date")]
        public DateTime StatusDate { get; set; }

        [Required]
        public RecordStatus Status { get; set; }

        public int AreaId { get; set; }

        public RecordFileCreateEditViewModel File { get; set; }
    }
}
