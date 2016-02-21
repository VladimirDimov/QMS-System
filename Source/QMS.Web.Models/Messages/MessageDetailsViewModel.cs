namespace QMS.Web.Models.Messages
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Users;

    public class MessageDetailsViewModel : IMapFrom<Message>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public UserShortViewModel Sender { get; set; }

        [DisplayName("Sent on")]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<UserShortViewModel> Users { get; set; }
    }
}
