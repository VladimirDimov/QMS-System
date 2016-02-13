using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Models
{
    public class Record
    {
        private ICollection<Note> notes;
        private ICollection<RecordFile> recordFiles;

        public Record()
        {
            this.notes = new HashSet<Note>();
            this.recordFiles = new HashSet<RecordFile>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? FinishingDate { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }

        [Required]
        public RecordStatus Status { get; set; }

        public int AreaId { get; set; }

        public virtual Area Area { get; set; }

        public virtual ICollection<Note> Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }

        public virtual ICollection<RecordFile> RecordFiles
        {
            get { return this.recordFiles; }
            set { this.recordFiles = value; }
        }
    }
}
