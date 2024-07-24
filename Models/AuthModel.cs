using System.ComponentModel.DataAnnotations;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Models
{
    public class AuthModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage ="Inserisci l'username, è obbligatorio.")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "l'username è di massimo 15 caratteri.")]
        public string Username {  get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Inserisci la password, è obbligatoria.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "La password è di massimo 10 caratteri.")]
        public string Password { get; set; }
    }
}
