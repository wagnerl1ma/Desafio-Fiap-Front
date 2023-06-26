using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CadastrosFiap.APP.ViewModels
{
    public class AlunoTurmaViewModel
    {
        //[Ignore]
        //[JsonIgnore]
        //public AlunoViewModel Aluno { get; set; }
        [Required(ErrorMessage = "É necessário colocar o {0}")]
        public int AlunoId { get; set; }

        //[Ignore]
        //[JsonIgnore]
        //public TurmaViewModel Turma { get; set; }

        [Required(ErrorMessage = "É necessário colocar o {0}")]
        public int TurmaId { get; set; }
    }
}
