using QMS.Models;
using QMS.Web.Infrastructure.Mappings;
using QMS.Web.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Procedures
{
    public class ProcedureDetailsModel : IMapFrom<Procedure>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<DocumentShortModel> Documents { get; set; }
    }
}
