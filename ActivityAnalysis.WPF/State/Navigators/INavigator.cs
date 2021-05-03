using System;
using ActivityAnalysis.WPF.ViewModels;

namespace ActivityAnalysis.WPF.State.Navigators
{
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        
        event Action StateChanged;
    }
}