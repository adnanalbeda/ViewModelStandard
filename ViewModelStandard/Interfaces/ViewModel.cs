namespace ViewModelStandard.Interfaces.ViewModel
{
    using System.ComponentModel;
    using ViewModelStandard.Interfaces.Validator;

    interface IViewModel
        : INotifyPropertyChanged, IVMErrorInfoMessage, IDataErrorInfo, INotifyDataErrorInfo
    {
        bool SetProperty<T>(ref T storage, T value, string propertyName);
        bool SetProperty<T>(ref T storage, T value, IValidator<T> validationRule, string propertyName);
        bool SetProperty<T>(ref T storage, T value, IPropValidator<T> validationRule, string propertyName);
    }

    interface IVMErrorInfoMessage 
        : IDataErrorInfo
    {
        string DataErrorInfoMessage { get; }
    }

    interface IViewModelBasing 
        : IViewModel, INotifyPropertyChanging { }
}
