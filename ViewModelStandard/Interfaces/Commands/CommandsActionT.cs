namespace ViewModelStandard.Interfaces.Commands
{
    using System;
    
    public interface ICommandAction<T> 
        : ICommandBase
    {
        Action<T> execute { get; }
        Predicate<T> canExecute { get; }
    }

}

