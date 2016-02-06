using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Models
{
    public class Division
    {
        private ICollection<Department> departments;

        public Division()
        {
            this.departments = new HashSet<Department>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Department> Departments
        {
            get { return this.departments; }
            set { this.departments = value; }
        }
    }
}
