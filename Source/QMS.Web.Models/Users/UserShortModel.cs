namespace QMS.Web.Models.Users
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;

    public class UserShortModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
