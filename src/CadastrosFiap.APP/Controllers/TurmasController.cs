using AutoMapper;
using CadastrosFiap.APP.Services;
using CadastrosFiap.APP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CadastrosFiap.APP.Controllers
{
    public class TurmasController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFiapApiService _fiapApiService;

        public TurmasController(IMapper mapper, IFiapApiService fiapApiService)
        {
            _mapper = mapper;
            _fiapApiService = fiapApiService;
        }

        // GET: TurmasController
        public async Task<IActionResult> Index()
        {
            var getAllTurmas = await _fiapApiService.GetAllTurmas(GetToken());

            var turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(getAllTurmas);

            return View(turmas);
        }

        // GET: TurmasController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: TurmasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TurmaViewModel turmaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(turmaViewModel);
                }

                var createAluno = await _fiapApiService.CreateTurma(turmaViewModel, GetToken());

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TurmasController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não inserido!" });
            }

            var turma = await _fiapApiService.GetTurmaById(id, GetToken());

            var turmaViewModel = _mapper.Map<TurmaViewModel>(turma);
            if (turmaViewModel == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não existe!" });
            }

            return View(turmaViewModel);
        }

        // POST: TurmasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TurmaViewModel turmaViewModel)
        {
            if (id != turmaViewModel.Id)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não são iguais!" });
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var turma = await _fiapApiService.UpdateTurma(id, turmaViewModel, GetToken());
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { mensagem = ex.Message });
            }

            return View(turmaViewModel);
        }

        // GET: TurmasController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não inserido!" });
            }

            var turma = await _fiapApiService.GetTurmaById(id, GetToken());
            var turmaViewModel = _mapper.Map<TurmaViewModel>(turma);

            if (turmaViewModel == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não existe!" });
            }

            return View(turmaViewModel);
        }

        // POST: TurmasController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                await _fiapApiService.RemoveTurmaById(id, GetToken());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Error), new { mensagem = $"Erro ao Remover Turma - {ex.Message}" });
            }
        }

        private string GetToken()
        {
            var token = _fiapApiService.GetToken().GetAwaiter().GetResult();
            return $"Bearer {token}";
        }
    }
}
