using Hospital.Domain.Filters;
using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.EntityFramework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.ASP.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IGenericRepository<Department> _departmentGenericService;
        private readonly EntryDataService _entryDataService;

        public AppointmentController(IGenericRepository<Department> departmentGenericService, EntryDataService entryDataService)
        {
            _departmentGenericService = departmentGenericService;
            _entryDataService = entryDataService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Department> res = await _departmentGenericService.GetWithInclude(d => d.Type == DepartmentType.Ambulatory, d => d.Title);
            //TO-DO: проверка на дубликаты Title
            ViewBag.DepartmentList = new SelectList(res, "Title.Id", "Title.Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoctorsList(int titleId, bool isFirstFree, DateTime date)
        {
            var filter = new EntrySearchFilter()
            {
                DateTime = date,
                DepartmentType = DepartmentType.Ambulatory,
                IsFree = true,
                IsNearest = isFirstFree,
                IsDate = !isFirstFree,
            };

            IEnumerable<Entry> DoctorsEntries = await _entryDataService.FindDoctor(titleId, filter);
            return PartialView("_DoctorsPartial", DoctorsEntries);
        }
    }
}
