namespace QMS.Web.Models.Departments
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;

    public class DepartmentListViewModel : IMapFrom<Department>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DivisionId { get; set; }

        public Division Division { get; set; }
    }
}
