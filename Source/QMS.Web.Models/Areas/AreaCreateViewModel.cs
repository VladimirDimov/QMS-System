namespace QMS.Web.Models.Areas
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AreaCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [AllowHtml]
        public string Name { get; set; }

        [MaxLength(200)]
        [UIHint("TextArea")]
        [AllowHtml]
        public string Description { get; set; }

        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [DisplayName("Employee")]
        public string EmployeeId { get; set; }
    }
}
