using QMS.Models;
using QMS.Web.Infrastructure.Mappings;
using QMS.Web.Models.Areas;
using QMS.Web.Models.Divisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Departments
{
    public class DepartmentDetailsModel : IMapFrom<Department>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DivisionId { get; set; }

        public virtual DivisionsDetailsResponseModel Division { get; set; }

        public virtual ICollection<AreaDetailsModel> Areas { get; set; }
    }
}
