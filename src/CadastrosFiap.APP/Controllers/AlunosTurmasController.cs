using AutoMapper;
using CadastrosFiap.APP.Services;
using CadastrosFiap.APP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CadastrosFiap.APP.Controllers
{
    public class AlunosTurmasController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFiapApiService _fiapApiService;

        public AlunosTurmasController(IMapper mapper, IFiapApiService fiapApiService)
        {
            _mapper = mapper;
            _fiapApiService = fiapApiService;
        }

        // GET: AlunosTurmasController
        public async Task<IActionResult> Index()
        {
            var getAllAlunosTurmas = await _fiapApiService.GetAllAlunosTurmas(GetToken());
            var turmas = _mapper.Map<IEnumerable<AlunoTurmaViewModel>>(getAllAlunosTurmas);

            return View(turmas);
        }


        //GET: AlunosTurmasController/Create
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}

        //GET: AlunosTurmasController/Create
        public async Task<IActionResult> Create()
        {
            var getAllAlunos = await _fiapApiService.GetAllAlunos(GetToken());
            var getAllTurmas = await _fiapApiService.GetAllTurmas(GetToken());

            var alunos = _mapper.Map<IEnumerable<AlunoViewModel>>(getAllAlunos);
            var turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(getAllTurmas);

            var viewModel = new FormAlunoTurmaViewModel { Alunos = alunos, Turmas = turmas };

            return View(viewModel);
        }

        // POST: AlunosTurmasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormAlunoTurmaViewModel formTurmaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(formTurmaViewModel);
                }

                var turmaViewModel = new AlunoTurmaViewModel();
                turmaViewModel.AlunoId = formTurmaViewModel.AlunoTurma.AlunoId;
                turmaViewModel.TurmaId = formTurmaViewModel.AlunoTurma.TurmaId;

                var createAluno = await _fiapApiService.CreateAlunoTurma(turmaViewModel, GetToken());

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AlunosTurmasController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não inserido!" });
            }

            var turma = await _fiapApiService.GetAlunoTurmaById(id, GetToken());
            var turmaViewModel = _mapper.Map<AlunoTurmaViewModel>(turma);

            if (turmaViewModel == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não existe!" });
            }

            var getAllAlunos = await _fiapApiService.GetAllAlunos(GetToken());
            var getAllTurmas = await _fiapApiService.GetAllTurmas(GetToken());

            var alunos = _mapper.Map<IEnumerable<AlunoViewModel>>(getAllAlunos);
            var turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(getAllTurmas);

            var viewModel = new FormAlunoTurmaViewModel { AlunoTurma = turmaViewModel, Alunos = alunos, Turmas = turmas };

            return View(viewModel);
        }

        // POST: AlunosTurmasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FormAlunoTurmaViewModel formTurmaViewModel)
        {
            if (id != formTurmaViewModel.AlunoTurma.AlunoId)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não são iguais!" });
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var turmaViewModel = new AlunoTurmaViewModel();
                    turmaViewModel.AlunoId = formTurmaViewModel.AlunoTurma.AlunoId;
                    turmaViewModel.TurmaId = formTurmaViewModel.AlunoTurma.TurmaId;

                    var turma = await _fiapApiService.UpdateAlunoTurma(id, turmaViewModel, GetToken());

                    return RedirectToAction(nameof(Index));
                }

            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { mensagem = ex.Message });
            }

            return View(formTurmaViewModel);
        }

        // GET: AlunosTurmasController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não inserido!" });
            }

            var turma = await _fiapApiService.GetAlunoTurmaById(id, GetToken());
            var turmaViewModel = _mapper.Map<AlunoTurmaViewModel>(turma);

            if (turmaViewModel == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não existe!" });
            }

            return View(turmaViewModel);
        }

        // POST: AlunosTurmasController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                await _fiapApiService.RemoveAlunoTurmaById(id, GetToken());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Error), new { message = $"Erro ao Remover Aluno Turma - {ex.Message}" });
            }
        }

        private string GetToken()
        {
            var token = _fiapApiService.GetToken().GetAwaiter().GetResult();
            return $"Bearer {token}";
        }
    }
}
