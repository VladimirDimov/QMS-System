namespace QMS.Services
{
    using Contracts;
    using QMS.Data;
    using QMS.Models;
    using System;
    using System.Linq;

    public class UsersServices : IUsersServices
    {
        private IQmsData data;

        public UsersServices(IQmsData data)
        {
            this.data = data;
        }

        public IQueryable<User> All()
        {
            return this.data.Users.All();
        }

        public User Update(
            string id,
            string userName,
            string firstName,
            string lastName,
            string phoneNumber,
            string email)
        {
            var user = this.data.Users.GetById(id);

            user.UserName = userName;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            user.Email = email;

            this.data.SaveChanges();
            return user;
        }

        public User GetById(string id)
        {
            return this.data.Users.GetById(id);
        }

        public void SetUserToNoNewMessages(string id)
        {
            var user = this.data.Users.GetById(id);
            if (user.HasNewMessages)
            {
                user.HasNewMessages = false;
                this.data.SaveChanges();
            }
        }

        public void SetPassword(string userId, string password)
        {
            var user = this.data.Users.GetById(userId);
            user.PasswordHash = this.data.UserManager.PasswordHasher.HashPassword(password);
            user.SecurityStamp = Guid.NewGuid().ToString();

            this.data.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            this.data.Users.Delete(id);
            this.data.SaveChanges();
        }
    }
}
