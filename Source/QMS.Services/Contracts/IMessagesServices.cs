namespace QMS.Services.Contracts
{
    using QMS.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IMessagesServices
    {
        Message Create(string title, string content, string senderId, ICollection<string> receiversIds);

        IQueryable<Message> LoadUserChatHistory(string userId);
    }
}