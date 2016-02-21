namespace QMS.Web.ViewModels.Departments
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using QMS.Web.ViewModels.Areas;
    using QMS.Web.ViewModels.Divisions;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class DepartmentDetailsViewModel : IMapFrom<Department>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DivisionId { get; set; }

        [DisplayName("Division")]
        public DivisionDetailsViewModel Division { get; set; }

        public ICollection<AreaDetailsViewModel> Areas { get; set; }
    }
}
