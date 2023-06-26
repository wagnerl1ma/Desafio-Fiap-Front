using AutoMapper;
using CadastrosFiap.APP.Services;
using CadastrosFiap.APP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CadastrosFiap.APP.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IMapper _mapper;

        public AlunosController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: AlunosController
        public async Task<IActionResult> Index()
        {
            var getAllAlunos = await ApiAlunoService.GetAllAlunos();

            var alunos = _mapper.Map<IEnumerable<AlunoViewModel>>(getAllAlunos);

            return View(alunos);
        }


        // GET: AlunosController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: AlunosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoViewModel alunoViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(alunoViewModel);
                }

                var createAluno = await ApiAlunoService.CreateAluno(alunoViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AlunosController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não inserido!" });
            }

            var aluno = await ApiAlunoService.GetAlunoById(id);

            var alunoViewModel = _mapper.Map<AlunoViewModel>(aluno);
            if (alunoViewModel == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não existe!" });
            }

            return View(alunoViewModel);
        }

        // POST: AlunosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlunoViewModel alunoViewModel)
        {
            if (id != alunoViewModel.Id)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não são iguais!" });
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var aluno = await ApiAlunoService.UpdateAluno(alunoViewModel, id);
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { mensagem = ex.Message });
            }

            return View(alunoViewModel);
        }

        // GET: AlunosController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não inserido!" });
            }

            var aluno = await ApiAlunoService.GetAlunoById(id);
            var alunoViewModel = _mapper.Map<AlunoViewModel>(aluno);

            if (alunoViewModel == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não existe!" });
            }

            return View(alunoViewModel);
        }

        // POST: AlunosController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                if(await ApiAlunoService.RemoveById(id))
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Error), new { mensagem = "Erro ao Remover Aluno" });

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
    }
}
