namespace QMS.Web.Models.Users
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using Areas;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class UserDetailsModel : IMapFrom<User>
    {
        public string Email { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        public string Id { get; set; }

        [DisplayName("Phone")]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }

        public ICollection<AreaShortViewModel> Areas { get; set; }
    }
}
