using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.EntityFramework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospital.ASP.Controllers
{
	[Authorize]
	public class MedCardController : Controller
	{
		private readonly ITestDataService _testDataService;
		private readonly EntryDataService _entryDataService;

		public MedCardController(ITestDataService dataService, EntryDataService entryDataService)
		{
			_testDataService = dataService;
			_entryDataService = entryDataService;
		}

		public IActionResult Index()
		{
			return RedirectToAction("Tests");
		}

		public async Task<IActionResult> Tests()
		{
			if (int.TryParse(User.FindFirstValue("id"), out int id))
			{
				var testList = await _testDataService.GetTestData(id);
				if (testList.Count() == 0) ModelState.AddModelError("", "данные не найдены");
				return View(testList);
			}
			else
			{
				ModelState.AddModelError("", "ошибка идентификации, попробуйте повторную авторизацию");
				return View();
			}
		}

		public async Task<IActionResult> Entries()
		{
			if (int.TryParse(User.FindFirstValue("id"), out int id))
			{
				var entries = await _entryDataService.GetEntries(id);
				return View(entries);
			}
			else
			{
				ModelState.AddModelError("", "ошибка идентификации, попробуйте повторную авторизацию");
				return View();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RemoveEntry(int entryId)
		{
			if (int.TryParse(User.FindFirstValue("id"), out int id))
			{
				var res = await _entryDataService.GetWhere(e => e.Id == entryId && e.Patient.Id == id && e.InviteStatus == InviteStatus.Первичное);
				if (res.Any())
				{
					//TODO: возможен вариант несоответствия, когда запись имеет "родителя", но при этом отмечена как первичная, в этом случае выбрасывает исключение о каскадном удалении
					try
					{
						await _entryDataService.Delete(entryId);
						return Content("Запись успешно отменена");
					}
					catch (Exception ex)
					{
						return BadRequest($"Внутренняя ошибка. Данная запись является каскадной, удаление невозможно. {ex.Message}");
					}
				}
				else return NotFound("Запись не найдена или недостаточно прав");
			}
			else return BadRequest("Ошибка идентификации, попробуйте повторную авторизацию");
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
				var testList = await _testDataService.GetTestData(id);
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
