namespace QMS.Web.Models.Departments
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;

    public class DepartmentDetailsSmallResponseModel : IMapFrom<Department>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}