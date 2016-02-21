namespace QMS.Services
{
    using Contracts;
    using QMS.Data;
    using QMS.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MessagesServices : IMessagesServices
    {
        private IQmsData data;

        public MessagesServices(IQmsData data)
        {
            this.data = data;
        }

        public Message Create(string title, string content, string senderId,
            ICollection<string> receiversIds)
        {
            var message = new Message
            {
                Title = title,
                Content = content,
                CreatedOn = DateTime.UtcNow,
                SenderId = senderId
            };

            foreach (var userId in receiversIds)
            {
                var receiver = this.data.Users.GetById(userId);
                message.Users.Add(receiver);
                receiver.HasNewMessages = true;
            }

            this.data.Messages.Add(message);
            this.data.SaveChanges();
            return message;
        }

        public IQueryable<Message> LoadUserChatHistory(string userId)
        {
            var messages = this.data.Messages.All()
                .Where(m => m.SenderId == userId || m.Users.Any(u => u.Id == userId));

            return messages;
        }
    }
}
