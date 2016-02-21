namespace QMS.Web.Models.Procedures
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;

    public class ProcedureListViewModel : IMapFrom<Procedure>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
