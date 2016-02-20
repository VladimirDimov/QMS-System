using QMS.Models;
using QMS.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QMS.Web.Models.Users
{
    public class UserEditViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [UIHint("String")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        [AllowHtml]
        public string UserName { get; set; }

        [MaxLength(50)]
        [AllowHtml]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [AllowHtml]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [MaxLength(30)]
        [Display(Name = "Phone")]
        [AllowHtml]
        public string PhoneNumber { get; set; }
    }
}
