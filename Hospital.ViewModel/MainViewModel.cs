using System.ComponentModel;

namespace Hospital.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged

    {
        internal static int CurrentStuffId { get; set; }

        private bool _isLoading = false;
        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
