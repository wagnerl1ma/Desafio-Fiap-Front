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
        private readonly IFiapApiService _fiapApiService;

        public AlunosController(IMapper mapper, IFiapApiService fiapApiService)
        {
            _mapper = mapper;
            _fiapApiService = fiapApiService;
        }

        // GET: AlunosController
        public async Task<IActionResult> Index()
        {
            var getAllAlunos = await _fiapApiService.GetAllAlunos(GetToken());
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

                var createAluno = await _fiapApiService.CreateAluno(alunoViewModel, GetToken());
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

            //var aluno = await ApiAlunoService.GetAlunoById(id);
            var aluno = await _fiapApiService.GetAlunoById(id, GetToken());


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
                    //var aluno = await ApiAlunoService.UpdateAluno(alunoViewModel, id);
                    var aluno = await _fiapApiService.UpdateAluno(id, alunoViewModel, GetToken());
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

            var aluno = await _fiapApiService.GetAlunoById(id, GetToken());
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
                await _fiapApiService.RemoveAlunoById(id, GetToken());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Error), new { message = $"Erro ao Remover Aluno - {ex.Message}" });
            }
        }

        private string GetToken()
        {
            var token = _fiapApiService.GetToken().GetAwaiter().GetResult();
            return $"Bearer {token}";
        }
    }
}
