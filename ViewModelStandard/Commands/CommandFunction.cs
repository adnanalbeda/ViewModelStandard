namespace ViewModelStandard.Commands
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using ViewModelStandard.Interfaces.Commands;

    public class CommandFunction<TResult> : ICommandFunction<TResult>
    {
        public ICommand Command => this;

        public CommandFunction(Func<TResult> execute) : this(execute, null) { }
        public CommandFunction(Func<TResult> execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? (() => true);
        }

        public Func<bool> canExecute { get; private set; }
        public Func<TResult> execute { get; private set; }

        public TResult Result { get; protected set; }

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
            Result = execute();
            OnPropertyChanged("Result");
        }

        #endregion
        #region PropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}