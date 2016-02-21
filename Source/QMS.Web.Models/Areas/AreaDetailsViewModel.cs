using QMS.Models;
using QMS.Web.Infrastructure.Mappings;
using QMS.Web.Models.Records;
using QMS.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Areas
{
    public class AreaDetailsViewModel : IMapFrom<Area>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string EmployeeId { get; set; }

        public UserDetailsViewModel Employee { get; set; }

        public ICollection<RecordListViewModel> Records { get; set; }
    }
}
