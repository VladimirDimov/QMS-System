namespace QMS.Web.Models.Users
{
    using Qms.Common;
    using System.ComponentModel.DataAnnotations;

    public class UserResetPasswordFromAdminViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [MinLength(UserConstants.PasswordMinLength)]
        [DataType(DataType.Password)]
        [UIHint("Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [UIHint("Password")]
        [Display(Name = "Repeat password")]
        public string RepeatPassword { get; set; }
    }
}
