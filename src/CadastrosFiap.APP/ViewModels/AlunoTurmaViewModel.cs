using System.Text.Json.Serialization;

namespace CadastrosFiap.APP.ViewModels
{
    public class AlunoTurmaViewModel
    {
        //[Ignore]
        [JsonIgnore]
        public AlunoViewModel Aluno { get; set; }
        public int AlunoId { get; set; }

        //[Ignore]
        [JsonIgnore]
        public TurmaViewModel Turma { get; set; }

        public int TurmaId { get; set; }
    }
}
