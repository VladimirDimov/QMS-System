using QMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Records
{
    public class RecordListDetails
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Document Document { get; set; }

        public DateTime DateRecorded { get; set; }

        public DateTime FinishingDate { get; set; }

        public DateTime StatusDate { get; set; }

        public RecordStatus Status { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
