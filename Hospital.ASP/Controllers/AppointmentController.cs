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
            //TODO: проверка на дубликаты Title, на данный момент их нет, но такое допускается
            IEnumerable<Department> departments = await _departmentGenericService.GetWithInclude(d => d.Type == DepartmentType.Ambulatory, d => d.Title);
            ViewBag.DepartmentList = new SelectList(departments, "Title.Id", "Title.Title");

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
        public async Task<IActionResult> ConfirmEntry(int doctorId, DateTime dateTimeUTC)
        {
            var dateTime = dateTimeUTC.ToLocalTime();

            Entry entry = (await _entryDataService.GetEntries(doctorId, dateTime.Date))
                .FirstOrDefault(e => e.TargetDateTime == dateTime);

            if (entry != null)
                if (entry.Id == 0)
                    if (int.TryParse(User.FindFirstValue("Id"), out int patientId))
                    {
                        Patient patient = await _patientGenericService.GetById(patientId);
                        if (patient != null)
                        {
                            entry.Patient = patient;
                            entry.Registrator = entry.DoctorDestination; //---заглушка отсутсвия данных сайта как регистратора, возможно оставить null
                            entry.EntryStatus = EntryStatus.Ожидание;
                            try
                            {
                                entry = await _entryDataService.Update(entry.Id, entry);
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError("", $"Ошибка работы сервиса данных: {ex.Message}");
                            }
                        }
                        else ModelState.AddModelError("", "Ошибка авторизации, пользователь не найден");
                    }
                    else ModelState.AddModelError("", "Ошибка cookie, попробуйте повторную авторизацию");
                else ModelState.AddModelError("", "Ошибка создания записи, запись уже создана");
            else ModelState.AddModelError("", "Ошибка создания записи, записей на текущее время нет");
            return PartialView("_ConfirmPartial", entry);
        }
    }
}
