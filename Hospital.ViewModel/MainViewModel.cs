using Hospital.EntityFramework;
using Hospital.ViewModel.Factories;
using System;
using System.ComponentModel;

namespace Hospital.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        internal static int CurrentStuffId { get; set; }

        private static string _headerCaption = "CodeBLUE Assistant Preview 0.1";
        public static string HeaderCaption { get => _headerCaption; set { _headerCaption = value; NotifyStaticPropertyChanged(nameof(HeaderCaption)); } }

        private bool _isLoading = false;


        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged = delegate { };
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
