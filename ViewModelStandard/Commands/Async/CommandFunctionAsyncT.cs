namespace ViewModelStandard.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModelStandard.Interfaces.Commands;


    public class CommandFunctionAsync<T, TResult> : CommandFunction<T, TResult>, ICommandFunctionAsync<T, TResult>
    {

        public CommandFunctionAsync(Func<T, TResult> execute) : this(execute, null) { }
        public CommandFunctionAsync(Func<T, TResult> execute, Predicate<T> canExecute) : base(execute, canExecute)
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
                OnPropertyChanged("Status");
            }
        }

        public Exception FaultedException { get; private set; }

        public async Task<TResult> ExecuteAsync(T parameter)
        {
            TResult result = default;
            Status = TaskStatus.Created;
            try
            {
                IsBusy = true;
                Status = TaskStatus.Running;
                result = await Task.Run(() => execute(parameter));
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
            return parameter is T ? canExecute((T)parameter) : false;
        }

        public override async void Execute(object parameter)
        {
            if (execute is null)
                return;
            if (parameter is T)
            {
                this.Result = await ExecuteAsync((T)parameter);
                OnPropertyChanged("Result");
            }
        }

        #endregion
    }
}







