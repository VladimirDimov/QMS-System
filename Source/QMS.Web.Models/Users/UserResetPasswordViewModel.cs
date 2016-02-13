namespace QMS.Web.Models.Users
{
    using Qms.Common;
    using System.ComponentModel.DataAnnotations;

    public class UserResetPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [MinLength(UserConstants.PasswordMinLength)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
    }
}
