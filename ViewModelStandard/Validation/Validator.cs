namespace ViewModelStandard.Validation
{
    using System;
    using ViewModelStandard.Interfaces.Validator;

    public abstract class ValidatorBase<T> : IValidator<T>
    {
        internal ValidatorBase(Predicate<T> rule, string errorMessage, bool isWarning)
        {
            ValidtationRule = rule;
            ErrorMessage = errorMessage;
            IsWarning = isWarning;
        }
        public Predicate<T> ValidtationRule { get; }

        public string ErrorMessage { get; }

        public bool IsWarning { get; }
    }

    public class ErrorValidator<T> : ValidatorBase<T>
    {
        public ErrorValidator(Predicate<T> rule, string errorMessage) 
            : base (rule, errorMessage, false) { }
    }

    public class WarningValidator<T> : ValidatorBase<T>
    {
        public WarningValidator(Predicate<T> rule, string errorMessage)
            : base(rule, errorMessage, true) { }
    }
}
