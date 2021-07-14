using System.Threading.Tasks;

namespace Hospital.ViewModel.Factories
{
    public interface IRootViewModelFactory<TViewModelType> where TViewModelType : MainViewModel
    {
        TViewModelType CreateViewModel();
    }
}
