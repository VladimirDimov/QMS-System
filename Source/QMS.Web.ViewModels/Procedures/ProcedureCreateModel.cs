namespace QMS.Web.ViewModels.Procedures
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ProcedureCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        [AllowHtml]
        public string Name { get; set; }

        [MaxLength(200)]
        [AllowHtml]
        [UIHint("TextArea")]
        public string Description { get; set; }
    }
}
