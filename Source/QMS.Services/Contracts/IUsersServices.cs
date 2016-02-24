namespace QMS.Services.Contracts
{
    using QMS.Models;
    using System.Linq;

    public interface IUsersServices
    {
        IQueryable<User> All();

        User GetById(string id);

        void SetPassword(string userId, string password);

        void SetUserToNoNewMessages(string id);

        User Update(string id, string userName, string firstName, string lastName, string phoneNumber, string email);

        void DeleteUser(string id);
    }
}