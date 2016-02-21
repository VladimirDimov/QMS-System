namespace QMS.Web.ViewModels.Users
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    class UserManageViewModel

    {
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [AllowHtml]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [AllowHtml]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [AllowHtml]
        [DisplayName("Phone")]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        [AllowHtml]
        [DisplayName("Username")]
        public string UserName { get; set; }
    }
}
