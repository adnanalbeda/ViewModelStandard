namespace ViewModelStandard.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModelStandard.Interfaces.Commands;

    public class CommandFunctionAsync<TResult> : CommandFunction<TResult>, ICommandFunctionAsync<TResult>
    {

        public CommandFunctionAsync(Func<TResult> execute) : this(execute, null) { }
        public CommandFunctionAsync(Func<TResult> execute, Func<bool> canExecute) : base(execute, canExecute)
        {
        }

        private TaskStatus taskStatus = TaskStatus.WaitingToRun;
        public TaskStatus Status
        {
            get => taskStatus;
            private set
            {
                taskStatus = value;
                OnPropertyChanged("Status");
            }
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

        public Exception FaultedException { get; private set; }

        public async Task<TResult> ExecuteAsync()
        {
            TResult result = default;
            Status = TaskStatus.Created;
            try
            {
                IsBusy = true;
                Status = TaskStatus.Running;
                result = await Task.Run(execute);
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
            return result;
        }

        #region ICommand Members 

        public override bool CanExecute(object parameter)
        {
            if (IsBusy)
                return false;
            return canExecute();
        }

        public override async void Execute(object parameter)
        {
            this.Result = await ExecuteAsync();
            OnPropertyChanged("Result");
        }

        #endregion
    }
}







