namespace ViewModelStandard.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModelStandard.Interfaces.Commands;

    public class CommandActionAsync : CommandAction, ICommandActionAsync
    {

        public CommandActionAsync(Action execute) : this(execute, null) { }
        public CommandActionAsync(Action execute, Func<bool> canExecute) : base(execute, canExecute)
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

        public async Task ExecuteAsync()
        {
            Status = TaskStatus.Created;
            try
            {
                IsBusy = true;
                Status = TaskStatus.Running;
                await Task.Run(execute);
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
            return canExecute();
        }

        public override async void Execute(object parameter)
        {
            await ExecuteAsync();
        }

        #endregion

        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}







