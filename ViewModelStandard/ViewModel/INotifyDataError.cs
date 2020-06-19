namespace ViewModelStandard
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;

    public abstract partial class ViewModel : INotifyDataErrorInfo
    {
        
        #region MSDN implementation, Source: SilverLight

        #region INotifyDataErrorInfo Members

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) ||
                !errors.ContainsKey(propertyName)) return null;
            return errors[propertyName];
        }

        public bool HasErrors => errors.Count > 0;
        
        #endregion

        private Dictionary<string, List<string>> errors =
            new Dictionary<string, List<string>>();

        // Adds the specified error to the errors collection if it is not 
        // already present, inserting it in the first position if isWarning is 
        // false. Raises the ErrorsChanged event if the collection changes. 
        protected virtual void AddError(string propertyName, string errorMessage, bool isWarning)
        {
            if (!errors.ContainsKey(propertyName))
                errors[propertyName] = new List<string>();

            if (!errors[propertyName].Contains(errorMessage))
            {
                if (isWarning) errors[propertyName].Add(errorMessage);
                else errors[propertyName].Insert(0, errorMessage);
                RaiseErrorsChanged(propertyName);
            }
        }

        // Removes the specified error from the errors collection if it is
        // present. Raises the ErrorsChanged event if the collection changes.
        protected virtual void RemoveError(string propertyName, string errorMessage)
        {
            if (errors.ContainsKey(propertyName) &&
                errors[propertyName].Contains(errorMessage))
            {
                errors[propertyName].Remove(errorMessage);
                if (errors[propertyName].Count == 0) errors.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }

        private void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke
                (this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

        /// <summary>
        /// To Change property and raising PropertyChanged and ErrorsChanged events.
        /// </summary>
        public bool SetProperty<T>(ref T storage, T value, Interfaces.Validator.IValidator<T> validator, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            if (!validator.ValidtationRule(value))
            {
                AddError(propertyName, validator.ErrorMessage, validator.IsWarning);
                changeProp(ref storage, value, propertyName);
                return false;
            }
            RemoveError(propertyName, validator.ErrorMessage);
            changeProp(ref storage, value, propertyName);
            return true;
        }

        /// <summary>
        /// To Validate and Change property then raising property changed events.
        /// </summary>
        public bool SetProperty<T>(ref T storage, T value, Interfaces.Validator.IPropValidator<T> validator, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            if (!validator.IsValid(value))
            {
                changeProp(ref storage, value, propertyName);
                return false;
            }
            changeProp(ref storage, value, propertyName);
            return true;
        }
    }
}