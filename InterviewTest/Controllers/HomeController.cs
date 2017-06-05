using InterviewTest.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InterviewTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMainModel _mainModel;

        public HomeController(IMainModel mainModel)
        {
            _mainModel = mainModel;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _mainModel.GetSortedCatList();
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
