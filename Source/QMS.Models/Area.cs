using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Models
{
    class Area
    {
        private ICollection<Record> records;

        public Area()
        {
            this.records = new HashSet<Record>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int EmplayeeId { get; set; }

        public virtual User Employee { get; set; }

        public virtual ICollection<Record> Records
        {
            get { return this.records; }
            set { this.records = value; }
        }
    }
}
