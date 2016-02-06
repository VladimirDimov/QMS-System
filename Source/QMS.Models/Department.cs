using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Models
{
    class Department
    {
        private ICollection<Area> areas;

        public Department()
        {
            this.areas = new HashSet<Area>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

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
