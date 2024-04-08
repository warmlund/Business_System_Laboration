using System.Windows.Input;

namespace Business_System_Laboration_4
{
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
            this.methodToExecute();
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
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}