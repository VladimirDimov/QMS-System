using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Models
{
    public class Department
    {
        private ICollection<Area> areas;

        public Department()
        {
            this.areas = new HashSet<Area>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public int DivisionId { get; set; }

        public virtual Division Division { get; set; }

        public virtual ICollection<Area> Areas
        {
            get { return this.areas; }
            set { this.areas = value; }
        }
    }
}
