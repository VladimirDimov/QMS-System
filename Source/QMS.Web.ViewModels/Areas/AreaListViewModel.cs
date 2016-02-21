using QMS.Models;
using QMS.Web.Infrastructure.Mappings;
using QMS.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.ViewModels.Areas
{
    public class AreaListViewModel : IMapFrom<Area>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual UserShortViewModel Employee { get; set; }
    }
}
