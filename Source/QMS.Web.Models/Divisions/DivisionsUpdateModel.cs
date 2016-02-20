namespace QMS.Web.Models.Divisions
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class DivisionsUpdateModel : IMapFrom<Division>
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
