namespace QMS.Web.Models.Divisions
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;

    public class DivisionListViewModel : IMapFrom<Division>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
