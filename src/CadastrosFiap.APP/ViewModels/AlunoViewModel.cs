using System.ComponentModel.DataAnnotations;

namespace CadastrosFiap.APP.ViewModels
{
    public class AlunoViewModel
    {
        //[Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É necessário colocar o {0}")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O tamanho do {0} deve ser entre {2} e {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É necessário colocar o {0}")]
        [StringLength(45, MinimumLength = 2, ErrorMessage = "O tamanho do {0} deve ser entre {2} e {1} caracteres!")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "É necessário colocar a {0}")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "O tamanho da {0} deve ser entre {2} e {1} caracteres com pelo menos 1 caractere especial!")]
        [RegularExpression(@"^(?=.*[^a-zA-Z0-9]).{8,40}$", ErrorMessage = "A senha deve atender aos requisitos: deve ser entre 8 e 40 caracteres com pelo menos 1 caractere especial")]
        public string Senha { get; set; }

    }
}
