using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospital.ASP.Controllers
{
    [Authorize]
    public class MedCardController : Controller
    {
        private readonly ITestDataService _dataService;


        public MedCardController(ITestDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Tests()
        {
            if (!User.HasClaim(p => p.Type == "id")) ModelState.AddModelError("", "claim:Id не найден");
            else if (!int.TryParse(User.FindFirstValue("id"), out int id)) ModelState.AddModelError("", "ошибка парсинга claim:Id");
            else
            {
                var testList = await _dataService.GetTestData(id);
                if (testList.Count() == 0) ModelState.AddModelError("", "данные не найдены");
                return View(testList);
            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<PartialViewResult> Details(int testId)
        {
            TestData test = new TestData();
            if (!User.HasClaim(p => p.Type == "id")) ModelState.AddModelError("", "claim:Id не найден");
            else if (!int.TryParse(User.FindFirstValue("id"), out int id)) ModelState.AddModelError("", "ошибка парсинга claim:Id");
            else
            {
                var testList = await _dataService.GetTestData(id);
                if (testList.Count() == 0) ModelState.AddModelError("", "данные не найдены");
                else
                {
                    test = testList.Where(t => t.Id == testId).FirstOrDefault();
                }
                
            };
            return PartialView("_DetailPartial", test);
        }
    }
}
