namespace ViewModelStandard
{
    using System;
    using System.ComponentModel;
    using ViewModelStandard.Interfaces.ViewModel;

    public abstract partial class ViewModel : IVMErrorInfoMessage, IDataErrorInfo 
    {

        #region IDataErrorInfo Members

        public string Error => DataErrorInfoMessage;

        public string this[string propertyName]
        {
            get
            {
                return (!errors.ContainsKey(propertyName) ? null :
                    String.Join(Environment.NewLine, errors[propertyName]));
            }
        }

        #endregion

        public virtual string DataErrorInfoMessage => "Some values don't meet validation requirements!";

    }
}