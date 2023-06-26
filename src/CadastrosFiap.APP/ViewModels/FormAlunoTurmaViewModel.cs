using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CadastrosFiap.APP.ViewModels
{
    public class FormAlunoTurmaViewModel
    {

        public AlunoTurmaViewModel AlunoTurma { get; set; }


        [Ignore]
        [Required(ErrorMessage = "É necessário selecionar um Aluno")]
        public IEnumerable<AlunoViewModel> Alunos { get; set; } = new List<AlunoViewModel>();

        [Ignore]
        [Required(ErrorMessage = "É necessário selecionar uma Turma")]
        public IEnumerable<TurmaViewModel> Turmas { get; set; } = new List<TurmaViewModel>();

    }
}
