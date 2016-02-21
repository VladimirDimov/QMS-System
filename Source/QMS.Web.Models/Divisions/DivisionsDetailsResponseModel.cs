namespace QMS.Web.Models.Divisions
{
    using Infrastructure.Mappings;
    using QMS.Models;
    using QMS.Web.Models.Departments;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class DivisionDetailsViewModel : IMapFrom<Division>
    {
        public int Id { get; set; }

        [DisplayName("Division name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<DepartmentShortViewModel> Departments { get; set; }
    }
}
