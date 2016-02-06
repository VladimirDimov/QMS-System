using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Models
{
    class Procedure
    {
        private ICollection<Document> documents;

        public Procedure()
        {
            this.documents = new HashSet<Document>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FilePath { get; set; }

        public virtual ICollection<Document> Documents
        {
            get { return this.documents; }
            set { this.documents = value; }
        }
    }
}
