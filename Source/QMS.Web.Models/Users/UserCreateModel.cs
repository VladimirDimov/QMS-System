using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Web.Models.Users
{
    public class UserCreateModel
    {
        [Required]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Compare("Email")]
        public string EmailConfirmed { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
