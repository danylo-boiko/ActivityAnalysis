using System;
using System.Windows.Input;
using ActivityAnalysis.WPF.State.Navigators;

namespace ActivityAnalysis.WPF.Commands
{
    public class RenavigateCommand : ICommand
    {
        private readonly IRenavigator _renavigator;
        public event EventHandler CanExecuteChanged;

        public RenavigateCommand(IRenavigator renavigator)
        {
            _renavigator = renavigator;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _renavigator.Renavigate();
        }
    }
}