namespace ViewModelStandard.Interfaces.Commands
{
    using System;

    public interface ICommandAction 
        : ICommandBase
    {
        Action execute { get; }
        Func<bool> canExecute { get; }
    }
    
}

