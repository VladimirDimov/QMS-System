namespace QMS.Web.Models.Users
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using Areas;
    using System.Collections.Generic;

    public class UserDetailsModel : IMapFrom<User>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Id { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string UserName { get; set; }

        public ICollection<AreaShortModel> Areas { get; set; }
    }
}
