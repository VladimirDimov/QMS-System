using QMS.Data;
using QMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Services
{
    public class MessagesServices
    {
        private IQmsData data;

        public MessagesServices(IQmsData data)
        {
            this.data = data;
        }

        public Message Create(string title, string content, string senderId,
            ICollection<User> receivers)
        {
            var message = new Message
            {
                Title = title,
                Content = content,
                CreatedOn = DateTime.UtcNow,
                SenderId = senderId
            };

            this.data.SaveChanges();

            foreach (var user in receivers)
            {
                message.Users.Add(user);
            }

            this.data.Messages.Add(message);
            this.data.SaveChanges();
            return message;
        }
    }
}
