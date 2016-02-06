using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Models
{
    public class Record
    {
        private ICollection<Note> notes;

        public Record()
        {
            this.notes = new HashSet<Note>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Document Document { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime FinishingDate { get; set; }

        public RecordStatus Status { get; set; }

        public ICollection<Note> Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }
    }
}
