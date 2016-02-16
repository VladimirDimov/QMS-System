namespace QMS.Web.Models.Messages
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using Users;
    using System;
    using System.Collections.Generic;

    public class MessageDetailsViewModel : IMapFrom<Message>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public UserShortModel Sender { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<UserShortModel> Users { get; set; }
    }
}
