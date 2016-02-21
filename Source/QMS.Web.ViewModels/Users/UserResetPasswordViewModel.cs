namespace QMS.Web.ViewModels.Users
{
    using Qms.Common;
    using System.ComponentModel.DataAnnotations;

    public class UserResetPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [UIHint("Password")]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

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
