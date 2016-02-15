using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Notes
{
    public class NoteCreateModel
    {
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; }

        public int RecordId { get; set; }
    }
}
