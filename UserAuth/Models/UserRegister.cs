using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserAuth.Models
{
    public class UserRegister
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        [Display(Name ="Email")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="{0} is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0} not a correct format")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",ErrorMessage ="{0} not a correct format")]
        [Remote("IsExit","Account",HttpMethod ="POST", ErrorMessage ="{0} is already exits")]
        public string UserName { get; set; } = string.Empty;
        [Display(Name ="Password")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="{0} is requird")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Display(Name ="Confirm Password")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="{0} is required")]
        [Compare(nameof(Password),ErrorMessage ="{0} and {1} dos't match. please try again")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}