namespace QMS.Web.Models.Departments
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;

    public class DepartmentShortViewModel : IMapFrom<Department>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}