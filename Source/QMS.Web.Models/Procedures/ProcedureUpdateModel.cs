namespace QMS.Web.Models.Procedures
{
    using System.ComponentModel.DataAnnotations;

    public class ProcedureUpdateModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
