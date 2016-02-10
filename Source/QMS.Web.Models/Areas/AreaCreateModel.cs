using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Areas
{
    public class AreaCreateModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        [UIHint("TextArea")]
        public string Description { get; set; }

        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [DisplayName("Employee")]
        public string EmployeeId { get; set; }
    }
}
