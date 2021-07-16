using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    public class AmbulatoryViewModel : MainViewModel
    {
        private readonly AmbulatoryDataService _ambulatoryDataService;

        public AmbulatoryViewModel(int entryId, 
            AmbulatoryDataService ambulatoryDataService,
            DiagnosticViewModel diagnosticViewModel,
            TherapyViewModel therapyViewModel,
            EntryViewModel entryViewModel)
        {
            _ambulatoryDataService = ambulatoryDataService;
            _diagnosticViewModel = diagnosticViewModel;
            _therapyViewModel = therapyViewModel;
            _entryViewModel = entryViewModel;

            EntryId = entryId;
            GetEntry(entryId).ConfigureAwait(true);
        }

        private bool _isEditable;
        private string _caption;
        private Entry _currentEntry;
        private DiagnosticViewModel _diagnosticViewModel;
        private TherapyViewModel _therapyViewModel;
        private EntryViewModel _entryViewModel;

        private int _entryId;

        private async Task GetEntry(int entryId)
        {
            IsLoading = true;
            try
            {

                CurrentEntry = await _ambulatoryDataService.GetEntryById(entryId);
                if (CurrentEntry.EntryStatus == Enum.Parse<EntryStatus>("3")) IsEditable = true;
                else IsEditable = false;
                if (CurrentEntry.MedCard == null) CurrentEntry.MedCard = new MedCard { Patient = CurrentEntry.Patient };
                await Task.WhenAll(DiagnosticViewModel.Initialize(CurrentEntry), TherapyViewModel.Initialize(CurrentEntry));
                EntryViewModel.Initialize(CurrentEntry);
                Caption = CurrentEntry.TargetDateTime.ToShortTimeString();
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 5);
            }
            IsLoading = false;
        }

        public string Caption { get => _caption; private set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public bool IsEditable { get => _isEditable; private set { _isEditable = value; OnPropertyChanged(nameof(IsEditable)); } }

        public Entry CurrentEntry { get => _currentEntry; private set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }
        public DiagnosticViewModel DiagnosticViewModel { get => _diagnosticViewModel; private set { _diagnosticViewModel = value; OnPropertyChanged(nameof(DiagnosticViewModel)); } }
        public TherapyViewModel TherapyViewModel { get => _therapyViewModel; private set { _therapyViewModel = value; OnPropertyChanged(nameof(TherapyViewModel)); } }
        public EntryViewModel EntryViewModel { get => _entryViewModel; private set { _entryViewModel = value; OnPropertyChanged(nameof(EntryViewModel)); } }
        public int EntryId { get => _entryId; set { _entryId = value; OnPropertyChanged(nameof(EntryId)); } }

        public async Task SaveChanges()
        {
            EntryViewModel.ApplyNextEntry();
            foreach (TestData data in DiagnosticViewModel.AddedDatas)
                if (data.Test.TestType.TestMethod == TestMethod.Физикальная) data.Status = ProcedureStatus.Готов;
                else data.Status = ProcedureStatus.Ожидание;
            foreach (PhysioTherapyData data in TherapyViewModel.AddedDatas.Where(d => d.GetType() == typeof(PhysioTherapyData))) data.ProcedureStatus = ProcedureStatus.Ожидание;
            foreach (SurgeryTherapyData data in TherapyViewModel.AddedDatas.Where(d => d.GetType() == typeof(SurgeryTherapyData))) data.ProcedureStatus = ProcedureStatus.Ожидание;

            var datas = new List<object>();
            datas.AddRange(DiagnosticViewModel.AddedDatas);
            datas.AddRange(TherapyViewModel.AddedDatas);
            datas.Add(EntryViewModel.CurrentEntry);
            datas.Add(EntryViewModel.CurrentEntry.MedCard);
            datas.Add(EntryViewModel.CurrentEntry.EntryOut);

            try
            {
                await _ambulatoryDataService.UpdateData(datas);
                NotificationManager.AddItem(new NotificationItem(NotificationType.Success, TimeSpan.FromSeconds(2), "Данные успешно сохранены\nЗапись создана"));
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }

            await Refresh();
        }

        public async Task Refresh()
        {
            await GetEntry(EntryId);
        }
    }
}
