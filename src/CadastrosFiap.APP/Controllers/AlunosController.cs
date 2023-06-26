using AutoMapper;
using CadastrosFiap.APP.Services;
using CadastrosFiap.APP.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            var getAllAlunos = await AlunoApiService.GetAllAlunos();

            var alunos = _mapper.Map<IEnumerable<AlunoViewModel>>(getAllAlunos);

            //var listaAluno = new List<AlunoViewModel>();
            //var aluno = new AlunoViewModel() { Id = 1, Nome = "Wagner", Usuario = "wagner10", Senha = "*******" };
            //listaAluno.Add(aluno);

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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AlunosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AlunosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AlunosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AlunosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
