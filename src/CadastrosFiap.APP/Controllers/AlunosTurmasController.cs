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

        public AlunosTurmasController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: AlunosTurmasController
        public async Task<IActionResult> Index()
        {
            var getAllTurmas = await ApiAlunoTurmaService.GetAllAlunosTurmas();

            var turmas = _mapper.Map<IEnumerable<AlunoTurmaViewModel>>(getAllTurmas);

            return View(turmas);
        }


        // GET: AlunosTurmasController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: AlunosTurmasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoTurmaViewModel turmaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(turmaViewModel);
                }
                var createAluno = await ApiAlunoTurmaService.CreateAlunoTurma(turmaViewModel);

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

            var turma = await ApiAlunoTurmaService.GetAlunoTurmaById(id);

            var turmaViewModel = _mapper.Map<AlunoTurmaViewModel>(turma);
            if (turmaViewModel == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não existe!" });
            }

            return View(turmaViewModel);
        }

        // POST: AlunosTurmasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlunoTurmaViewModel turmaViewModel)
        {
            if (id != turmaViewModel.AlunoId)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não são iguais!" });
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var turma = await ApiAlunoTurmaService.UpdateAlunoTurma(turmaViewModel, id);

                    return RedirectToAction(nameof(Index));
                }

            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { mensagem = ex.Message });
            }

            return View(turmaViewModel);
        }

        // GET: AlunosTurmasController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não inserido!" });
            }

            var turma = await ApiAlunoTurmaService.GetAlunoTurmaById(id);
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
                if (await ApiAlunoTurmaService.RemoveById(id))
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
