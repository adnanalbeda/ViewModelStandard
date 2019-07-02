namespace ViewModelStandard.Validation
{
    using System;
    using System.Collections.Generic;
    using ViewModelStandard.Interfaces.Validator;

    public class PropValidator<T> : List<IValidator<T>>, IPropValidator<T>
    {
        public PropValidator(string propName, 
            Action<string, string, bool> addError, Action<string, string> removeError)
        {
            this.PropName = propName;
            this.AddErrorToList = addError;
            this.RemoveErrorToList = removeError;
        }

        public string PropName { get; }

        /// <summary>
        /// AddError(propname, errormessage, isWarning)
        /// </summary>
        public Action<string, string, bool> AddErrorToList { get; }

        /// <summary>
        /// RemoveError(propname, errormessage)
        /// </summary>
        public Action<string, string> RemoveErrorToList { get; }

        public bool IsValid(T param)
        {
            bool valid = true;
            bool temp;
            foreach (var item in this)
            {
                temp = item.ValidtationRule(param);
                if (temp)
                    RemoveErrorToList(PropName, item.ErrorMessage);
                else
                    AddErrorToList(PropName, item.ErrorMessage, item.IsWarning);
                valid = valid && temp;
            }
            return valid;
        }

        public new void Add(IValidator<T> validator)
        {
            base.Add(validator);
        }
    }
}
