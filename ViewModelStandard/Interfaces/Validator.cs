namespace ViewModelStandard.Interfaces.Validator
{
    using System;
    using System.Collections.Generic;

    public interface IValidator<T>
    {
        Predicate<T> ValidtationRule { get; }
        string ErrorMessage { get; }
        bool IsWarning { get; }
    }

    public interface IPropValidator<T> : IEnumerable<IValidator<T>>
    {
        string PropName { get; }

        Action<string, string, bool> AddErrorToList { get; }
        Action<string, string> RemoveErrorToList { get; }

        bool IsValid(T param);
    }
}
