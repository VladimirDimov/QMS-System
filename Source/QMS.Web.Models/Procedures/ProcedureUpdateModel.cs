namespace QMS.Web.Models.Procedures
{
    using Infrastructure.Mappings;
    using QMS.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ProcedureUpdateViewModel : IMapFrom<Procedure>
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
