namespace QMS.Web.ViewModels.Procedures
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using QMS.Web.ViewModels.Documents;
    using System.Collections.Generic;

    public class ProcedureDetailsViewModel : IMapFrom<Procedure>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<DocumentShortViewModel> Documents { get; set; }
    }
}
