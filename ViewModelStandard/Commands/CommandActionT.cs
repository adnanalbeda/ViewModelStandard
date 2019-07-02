namespace ViewModelStandard.Commands
{
    using System;
    using System.Windows.Input;
    using ViewModelStandard.Interfaces.Commands;

    public class CommandAction<T> : ICommandAction<T>
    {
        public ICommand Command => this;

        public CommandAction(Action<T> execute) : this(execute, null) { }
        public CommandAction(Action<T> execute, Predicate<T> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? ((x) => true);
        }

        public Action<T> execute { get; private set; }
        public Predicate<T> canExecute { get; private set; }

        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region ICommand Members 

        public virtual event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return parameter is T ? canExecute((T)parameter) : false;
        }

        public virtual void Execute(object parameter)
        {
            if (parameter is T)
                execute((T)parameter);
        }

        #endregion
    }
}