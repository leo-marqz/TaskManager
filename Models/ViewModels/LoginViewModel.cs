using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recuerdame")]
        public bool RememberMe { get; set; }
    }
}
