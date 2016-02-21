namespace QMS.Web.ViewModels.Areas
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AreaEditViewModel : IMapFrom<Area>
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

        [DisplayName("Employee username")]
        public string EmployeeId { get; set; }
    }
}
