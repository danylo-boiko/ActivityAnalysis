using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ActivityAnalysis.WPF.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        private bool _isExecuting;
        public event EventHandler CanExecuteChanged;

        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }
        
        public async void Execute(object parameter)
        {
            IsExecuting = true;

            await ExecuteAsync(parameter);

            IsExecuting = false;
        }
        
        protected abstract Task ExecuteAsync(object parameter);

        public virtual bool CanExecute(object parameter) => !IsExecuting;
        
        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}