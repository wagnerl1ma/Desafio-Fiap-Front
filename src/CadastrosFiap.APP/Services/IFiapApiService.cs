using CadastrosFiap.APP.DTOs;
using CadastrosFiap.APP.ViewModels;
using Refit;

namespace CadastrosFiap.APP.Services
{
    public interface IFiapApiService
    {
        #region JWT Token

        [Get("/api/v1/autenticacao")]
        Task<string> GetToken();

        #endregion

        #region Alunos

        [Get("/api/v1/alunos")]
        Task<IEnumerable<AlunoDTO>> GetAllAlunos([Header("Authorization")] string token);

        [Get("/api/v1/alunos/{id}")]
        Task<AlunoDTO> GetAlunoById(int? id, [Header("Authorization")] string token);

        [Post("/api/v1/alunos")]
        Task<AlunoDTO> CreateAluno(AlunoViewModel alunoViewModel, [Header("Authorization")] string token);

        [Put("/api/v1/alunos/{id}")]
        Task<AlunoDTO> UpdateAluno(int id, AlunoViewModel alunoViewModel, [Header("Authorization")] string token);

        [Delete("/api/v1/alunos/{id}")]
        Task RemoveAlunoById(int? id, [Header("Authorization")] string token);

        #endregion


        #region Turmas

        [Get("/api/v1/turmas")]
        Task<IEnumerable<TurmaDTO>> GetAllTurmas([Header("Authorization")] string token);

        [Get("/api/v1/turmas/{id}")]
        Task<TurmaDTO> GetTurmaById(int? id, [Header("Authorization")] string token);

        [Post("/api/v1/turmas")]
        Task<TurmaDTO> CreateTurma(TurmaViewModel turmaViewModel, [Header("Authorization")] string token);

        [Put("/api/v1/turmas/{id}")]
        Task<TurmaDTO> UpdateTurma(int id, TurmaViewModel turmaViewModel, [Header("Authorization")] string token);

        [Delete("/api/v1/turmas/{id}")]
        Task RemoveTurmaById(int? id, [Header("Authorization")] string token);

        #endregion

        #region Alunos Turmas

        [Get("/api/v1/alunosturmas")]
        Task<IEnumerable<AlunoTurmaDTO>> GetAllAlunosTurmas([Header("Authorization")] string token);

        [Get("/api/v1/alunosturmas/{id}")]
        Task<AlunoTurmaDTO> GetAlunoTurmaById(int? id, [Header("Authorization")] string token);

        [Post("/api/v1/alunosturmas")]
        Task<AlunoTurmaDTO> CreateAlunoTurma(AlunoTurmaViewModel alunoTurmaViewModel, [Header("Authorization")] string token);

        [Put("/api/v1/alunosturmas/{id}")]
        Task<AlunoTurmaDTO> UpdateAlunoTurma(int id, AlunoTurmaViewModel alunoTurmaViewModel, [Header("Authorization")] string token);

        [Delete("/api/v1/alunosturmas/{id}")]
        Task RemoveAlunoTurmaById(int? id, [Header("Authorization")] string token);

        #endregion
    }
}
