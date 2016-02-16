namespace QMS.Web.Models.Procedures
{
    using QMS.Models;
    using Infrastructure.Mappings;
    using System.ComponentModel.DataAnnotations;

    public class ProcedureUpdateModel : IMapFrom<Procedure>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
