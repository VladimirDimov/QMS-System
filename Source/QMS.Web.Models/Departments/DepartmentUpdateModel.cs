namespace QMS.Web.Models.Departments
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class DepartmentUpdateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [AllowHtml]
        public string Name { get; set; }

        [MaxLength(200)]
        [UIHint("TextArea")]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        [DisplayName("Division")]
        [Range(1, (double)int.MaxValue, ErrorMessage = "The division is required")]
        public int DivisionId { get; set; }
    }
}
