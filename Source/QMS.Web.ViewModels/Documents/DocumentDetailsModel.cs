namespace QMS.Web.ViewModels.Documents
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using QMS.Web.ViewModels.Procedures;
    using System;
    using System.ComponentModel;

    public class DocumentDetailsViewModel : IMapFrom<Document>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string FilePath { get; set; }

        [DisplayName("Last update")]
        public DateTime LastUpdate { get; set; }

        public ProcedureShortViewModel Procedure { get; set; }
    }
}
