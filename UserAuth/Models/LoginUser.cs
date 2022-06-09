using System.ComponentModel.DataAnnotations;

namespace UserAuth.Models
{
    public class LoginUser
    {
        [Display(Name ="Email")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="{0} is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="{0} not a correct format")]
        public string UserName { get; set; } = string.Empty;
        [Display(Name ="Password")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="{0} is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }= string.Empty;
    }
}
