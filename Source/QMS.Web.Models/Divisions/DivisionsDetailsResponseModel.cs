namespace QMS.Web.Models.Divisions
{
    using QMS.Web.Infrastructure.Mappings;
    using QMS.Models;
    using QMS.Web.Models.Departments;
    using System.Collections.Generic;

    public class DivisionsDetailsResponseModel : IMapFrom<Division>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<DepartmentDetailsSmallResponseModel> Departments { get; set; }
    }
}
