using System.Windows.Input;

namespace Business_System_Laboration_4
{
    public class Command<T> : ICommand
    {
        private Action<T> methodToExecuteWithParam = null;
        private Func<T, bool> canMethodBeExecuted = null;
        private Action methodToExecute = null;

        public event EventHandler CanExecuteChanged;

        public Command(Action<T> methodToExecute, Func<T, bool> canMethodBeExecuted)
        {
            this.methodToExecuteWithParam = methodToExecute;
            this.canMethodBeExecuted = canMethodBeExecuted;
        }

        public void Execute(object parameter)
        {
            if (parameter != null && parameter is T typedParameter)
            {
                this.methodToExecuteWithParam?.Invoke(typedParameter);
            }
            else
            {
                this.methodToExecute?.Invoke();
            }
        }

        public bool CanExecute(object parameter)
        {
            if (parameter != null && parameter is T typedParameter)
            {
                return this.canMethodBeExecuted(typedParameter);
            }
            else
            {
                return false;
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Command : ICommand
    {
        private Action methodToExecute = null;
        private Func<bool> canMethodBeExecuted = null;

        public event EventHandler CanExecuteChanged;

        public Command(Action methodToExecute, Func<bool> canMethodBeExecuted)
        {
            this.methodToExecute = methodToExecute;
            this.canMethodBeExecuted = canMethodBeExecuted;
        }

        public void Execute(object parameter)
        {
            this.methodToExecute?.Invoke();
        }

        public bool CanExecute(object parameter)
        {
            if (this.canMethodBeExecuted == null)
            {
                return true;
            }
            else
            {
                return this.canMethodBeExecuted();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}