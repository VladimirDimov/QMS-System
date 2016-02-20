namespace QMS.Web.Models.Documents
{
    using Infrastructure.Mappings;
    using QMS.Models;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class DocumentUpdateModel : IMapFrom<Document>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [AllowHtml]
        public string Title { get; set; }

        [MaxLength(500)]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [DisplayName("Procedure")]
        public int ProcedureId { get; set; }
    }
}
