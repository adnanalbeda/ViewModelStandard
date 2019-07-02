namespace ViewModelStandard.Interfaces.Commands
{

    using System;

    public interface ICommandFunction<TResult> 
        : ICommandResultBase<TResult>
    {
        Func<TResult> execute { get; }
        Func<bool> canExecute { get; }
    }

}

