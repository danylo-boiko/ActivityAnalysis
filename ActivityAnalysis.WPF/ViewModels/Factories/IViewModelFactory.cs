using ActivityAnalysis.WPF.State.Navigators;

namespace ActivityAnalysis.WPF.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}