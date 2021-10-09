using Hospital.Domain.Filters;
using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.EntityFramework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospital.ASP.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IGenericRepository<Department> _departmentGenericService;
        private readonly IGenericRepository<Patient> _patientGenericService;
        private readonly EntryDataService _entryDataService;

        public AppointmentController(IGenericRepository<Department> departmentGenericService, EntryDataService entryDataService, IGenericRepository<Patient> departmentPatientService, IGenericRepository<Patient> patientGenericService)
        {
            _departmentGenericService = departmentGenericService;
            _entryDataService = entryDataService;
            _patientGenericService = patientGenericService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Department> res = await _departmentGenericService.GetWithInclude(d => d.Type == DepartmentType.Ambulatory, d => d.Title);
            //TODO: проверка на дубликаты Title
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

            IEnumerable<Entry> doctorsEntries = await _entryDataService.FindDoctor(titleId, filter);
            return PartialView("_DoctorsPartial", doctorsEntries);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EntriesList(int doctorId, DateTime date)
        {
            ViewBag.CurrentDate = date;
            ViewBag.CurrentDoctorId = doctorId;
            IEnumerable<Entry> entries = await _entryDataService.GetEntries(doctorId, date);
            return PartialView("_EntriesPartial", entries);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEntry(int doctorId, DateTime dateTime)
        {
            Entry entry = (await _entryDataService.GetEntries(doctorId, dateTime.Date))
                .FirstOrDefault(e => e.TargetDateTime == dateTime);
            if (entry != null)
            {
                
            }


            if (int.TryParse(User.FindFirstValue("Id"), out int patientId))
            {
                ViewBag.CurrentPatient = await _patientGenericService.GetById(patientId);
                //TODO: дополнительные проверки
            }
            else
            {
                ModelState.AddModelError("", "Ошибка cookie, попробуйте повторную авторизацию");
            }

            return PartialView("_ConfirmPartial");
        }
    }
}
