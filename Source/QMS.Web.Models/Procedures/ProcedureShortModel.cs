namespace QMS.Web.Models.Procedures
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;

    public class ProcedureShortModel : IMapFrom<Procedure>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
