namespace QMS.Web.ViewModels.Procedures
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using System.ComponentModel;

    public class ProcedureShortViewModel : IMapFrom<Procedure>
    {
        public int Id { get; set; }

        [DisplayName("Procedure Name")]
        public string Name { get; set; }
    }
}
