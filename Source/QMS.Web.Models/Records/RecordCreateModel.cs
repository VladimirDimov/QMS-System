using QMS.Models;
using QMS.Web.Models.RecordFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Records
{
    public class RecordCreateModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? FinishingDate { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }

        [Required]
        public RecordStatus Status { get; set; }

        public int AreaId { get; set; }

        public RecordFileCreateEditModel File { get; set; }
    }
}
