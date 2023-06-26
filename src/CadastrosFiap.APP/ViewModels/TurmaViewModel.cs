using System.ComponentModel.DataAnnotations;

namespace CadastrosFiap.APP.ViewModels
{
    public class TurmaViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Curso")]
        public int IdCurso { get; set; }

        [Required(ErrorMessage = "É necessário colocar o {0}")]
        [Display(Name = "Nome da Turma", Prompt = "Ex: Turma 2023")] //Prompt = Espaço Reservado - placeholder
        [StringLength(45, MinimumLength = 5, ErrorMessage = "O tamanho do {0} deve ser entre {2} e {1} caracteres!")]
        public string NomeTurma { get; set; }

        [Required(ErrorMessage = "É necessário colocar o {0}")]
        [Display(Name = "Ano da Turma", Prompt = "Ex: 2023")]
        public int Ano { get; set; }
    }
}
