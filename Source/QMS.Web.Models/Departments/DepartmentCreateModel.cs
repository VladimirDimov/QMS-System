namespace QMS.Web.Models.Departments
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class DepartmentCreateModel : IMapFrom<Department>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        [UIHint("TextArea")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Division")]
        [Range(1, (double)int.MaxValue,ErrorMessage = "The division is required")]
        public int DivisionId { get; set; }
    }
}
