using System.ComponentModel.DataAnnotations;

namespace PageManagementSystem.UI.Models.VMs
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email gerekli.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin.")]
        public string Email { get; set; }
    }
}
