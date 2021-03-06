using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModel
{
    public class LoginViewModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    public bool RememberMe { get; set; }
}
}