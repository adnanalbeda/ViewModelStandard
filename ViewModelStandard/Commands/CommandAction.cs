namespace ViewModelStandard.Commands
{
    using System;
    using System.Windows.Input;
    using ViewModelStandard.Interfaces.Commands;

    public class CommandAction : ICommandAction
    {
        public ICommand Command => this;

        public CommandAction(Action execute) : this(execute, null) { }
        public CommandAction(Action execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? (() => true);
        }

        public Func<bool> canExecute { get; private set; }
        public Action execute { get; private set; }

        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }       

        #region ICommand Members 

        public virtual event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public virtual void Execute(object parameter)
        {
            execute();
        }

        #endregion



    }
}