using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital.ViewModel.Extensions
{
    public class ObservableStorage<T> : ObservableCollection<T>
    {
        private bool _isLoading;

        private T _selected;
        private T _current;

        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsLoading))); } }

        public T Selected { get => _selected; set { _selected = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(Selected))); } }
        public T Current { get => _current; set { _current = value; OnPropertyChanged(new PropertyChangedEventArgs(nameof(Current))); } }
    }
}
