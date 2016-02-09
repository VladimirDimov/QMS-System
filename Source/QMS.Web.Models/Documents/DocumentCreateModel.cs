namespace QMS.Web.Models.Documents
{
    using QMS.Web.Models.Procedures;
    using System.ComponentModel.DataAnnotations;

    public class DocumentCreateModel
    {
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
