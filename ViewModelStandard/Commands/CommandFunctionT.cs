namespace ViewModelStandard.Commands
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using ViewModelStandard.Interfaces.Commands;

    public class CommandFunction<T, TResult> : ICommandFunction<T, TResult>
    {
        public ICommand Command => this;

        public CommandFunction(Func<T, TResult> execute) : this(execute, null) { }
        public CommandFunction(Func<T, TResult> execute, Predicate<T> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? ((x) => true);
        }

        public Predicate<T> canExecute { get; private set; }
        public Func<T, TResult> execute { get; private set; }

        public TResult Result { get; protected set; }

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
            {
                Result = execute((T)parameter);
                OnPropertyChanged("Result");
            }
        }

        #endregion

        #region INotifyOnPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}