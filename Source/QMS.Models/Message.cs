using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QMS.Models
{
    public class Message
    {
        private ICollection<User> users;

        public Message()
        {
            this.users = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Content { get; set; }

        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}
