namespace QMS.Web.Models.Documents
{
    using QMS.Models;
    using Infrastructure.Mappings;
    using System.ComponentModel.DataAnnotations;
    using Procedures;

    public class DocumentUpdateModel : IMapFrom<Document>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        public int ProcedureId { get; set; }
    }
}
