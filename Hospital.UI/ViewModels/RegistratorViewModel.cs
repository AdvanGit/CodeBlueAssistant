using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.UI.ViewModels
{
	public static class Extensions
	{
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
		{
			return new ObservableCollection<T>(col);
		}
	}

	public class RegistratorViewModel : INotifyPropertyChanged
	{
		public RegistratorViewModel()
		{
			RegTables = new List<UserControl>
			{
				new Controls.RegDoctorTable(),
				new Controls.RegPatientTable()
			};
			CurrentRegTable = RegTables.ElementAt(0);
		}

		private UserControl _currentRegTable;
		private Patient _selectedPatient;
		private Staff _selectedStaff;

		public UserControl CurrentRegTable { get => _currentRegTable; set { _currentRegTable = value; OnPropertyChanged(nameof(CurrentRegTable)); } }
		public Patient SelectedPatient { get => _selectedPatient; set { _selectedPatient = value; OnPropertyChanged(nameof(SelectedPatient)); } }
		public Staff SelectedStaff { get => _selectedStaff; set { _selectedStaff = value; OnPropertyChanged(nameof(SelectedStaff)); } }

		private RelayCommand _insertData;
		public RelayCommand InsertData { get => _insertData ?? (_insertData = new RelayCommand(obj => { Task.Run(() => GetData()); })); }

		public ObservableCollection<Staff> Doctors { get; set; }
		public ObservableCollection<Patient> Patients { get; set; }
		public List<UserControl> RegTables { get; set; }

		public async void GetData()
		{
			using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
			{
				var _patients = await db.Patients.Include(e => e.Belay).ToListAsync();
				Patients = _patients.ToObservableCollection();

				var _doctors = await db.Staffs.Include(s => s.Department).ThenInclude(d => d.Title).ToListAsync();
				Doctors = _doctors.ToObservableCollection();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
}
}
