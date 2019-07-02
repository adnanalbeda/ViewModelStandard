namespace ViewModelStandard.Interfaces.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface ICommandBase : ICommand
    {
        void RaiseCanExecuteChanged();
    }

    public interface ICommandResultBase<TResult> : ICommandBase, INotifyPropertyChanged
    {
        TResult Result { get; }
    }

    public interface ICommandAsyncBase : ICommandBase, INotifyPropertyChanged
    {
        bool IsBusy { get; }
        TaskStatus Status { get; }
        Exception FaultedException { get; }
    }
}

