using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Departments
{
    public class DepartmentUpdateModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        [UIHint("TextArea")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Division")]
        [Range(1, (double)int.MaxValue, ErrorMessage = "The division is required")]
        public int DivisionId { get; set; }
    }
}
