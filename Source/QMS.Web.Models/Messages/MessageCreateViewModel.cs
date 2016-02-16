using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Messages
{
    public class MessageCreateViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        [UIHint("TextArea")]
        public string Content { get; set; }

        [Required]
        public string ReceiverId { get; set; }
    }
}
