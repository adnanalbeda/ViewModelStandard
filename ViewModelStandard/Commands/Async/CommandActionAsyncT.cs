namespace ViewModelStandard.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModelStandard.Interfaces.Commands;

    public class CommandActionAsync<T> : CommandAction<T>, ICommandActionAsync<T>
    {

        public CommandActionAsync(Action<T> execute) : this(execute, null) { }
        public CommandActionAsync(Action<T> execute, Predicate<T> canExecute) : base(execute, canExecute)
        {
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                _isBusy = value;
                RaiseCanExecuteChanged();
            }
        }

        private TaskStatus taskStatus = TaskStatus.WaitingToRun;
        public TaskStatus Status
        {
            get => taskStatus;
            private set
            {
                taskStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
            }
        }

        public Exception FaultedException { get; private set; }

        public async Task ExecuteAsync(T parameter)
        {
            Status = TaskStatus.Created;
            try
            {
                IsBusy = true;
                Status = TaskStatus.Running;
                await Task.Run(() => execute(parameter));
                FaultedException = null;
                Status = TaskStatus.RanToCompletion;
            }
            catch (OperationCanceledException)
            {
                FaultedException = null;
                Status = TaskStatus.Canceled;
            }
            catch (Exception ex)
            {
                FaultedException = ex;
                Status = TaskStatus.Faulted;
            }
            finally
            {
                IsBusy = false;
            }
        }

        #region ICommand Members 

        public override bool CanExecute(object parameter)
        {
            if (IsBusy)
                return false;
            return parameter is T ? canExecute((T)parameter) : false;
        }

        public override async void Execute(object parameter)
        {
            if (parameter is T)
                await ExecuteAsync((T)parameter);
        }

        #endregion

        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

}







