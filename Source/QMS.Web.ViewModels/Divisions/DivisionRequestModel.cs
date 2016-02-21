namespace QMS.Web.ViewModels.Divisions
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class DivisionRequestViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [AllowHtml]
        public string Name { get; set; }

        [MaxLength(200)]
        [UIHint("TextArea")]
        [AllowHtml]
        public string Description { get; set; }
    }
}
