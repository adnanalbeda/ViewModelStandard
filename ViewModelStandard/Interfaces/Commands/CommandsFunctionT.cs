namespace ViewModelStandard.Interfaces.Commands
{

    using System;
    using System.ComponentModel;

    public interface ICommandFunction<T, TResult> 
        : ICommandResultBase<TResult>
    {
        Func<T, TResult> execute { get; }
        Predicate<T> canExecute { get; }
    }
}

