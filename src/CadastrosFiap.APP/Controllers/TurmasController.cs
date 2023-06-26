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

        public TurmasController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: TurmasController
        public async Task<IActionResult> Index()
        {
            var getAllTurmas = await ApiTurmaService.GetAllTurmas();

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

                var createAluno = await ApiTurmaService.CreateTurma(turmaViewModel);

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

            var turma = await ApiTurmaService.GetTurmaById(id);

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
                    var turma = await ApiTurmaService.UpdateTurma(turmaViewModel, id);
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

            var turma = await ApiTurmaService.GetTurmaById(id);
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
                if (await ApiTurmaService.RemoveById(id))
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Error), new { mensagem = "Erro ao Remover Turma" });

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
    }
}
