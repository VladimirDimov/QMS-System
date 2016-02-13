namespace QMS.Web.Models.Users
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using System.ComponentModel.DataAnnotations;

    public class UserShortModel : IMapFrom<User>
    {
        public string Id { get; set; }

        [DisplayFormat(NullDisplayText = "No user")]
        public string UserName { get; set; }
    }
}
