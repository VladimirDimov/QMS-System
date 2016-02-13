using QMS.Models;
using QMS.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Areas
{
    public class AreaEditModel : IMapFrom<Area>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        [UIHint("TextArea")]
        public string Description { get; set; }

        [DisplayName("Employee username")]
        public string EmployeeId { get; set; }
    }
}
