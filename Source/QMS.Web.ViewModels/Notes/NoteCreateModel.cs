namespace QMS.Web.ViewModels.Notes
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class NoteCreateViewModel
    {
        [MaxLength(50)]
        [AllowHtml]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        [AllowHtml]
        [UIHint("TextArea")]
        public string Text { get; set; }

        public int RecordId { get; set; }
    }
}
