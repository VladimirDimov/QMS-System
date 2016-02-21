namespace QMS.Web.ViewModels.Messages
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class MessageCreateViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [AllowHtml]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        [UIHint("TextArea")]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        [DisplayName("Send to")]
        public string ReceiverId { get; set; }
    }
}
