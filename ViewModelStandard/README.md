
# mvvmStandard Library

ViewModel for every platform.

MIT License - All rights reserved.

## About

mvvmStandard, as the name hint, is a standard library that can be used on every .net platform:

- .net Framework.
- .net Core.
- Xamarin.

Its main goal is to help in designing __ViewModel__, with a package of _ViewModelBase_, _Commands_, _Validators_, _CustomObservableCollection_ and _Snippets_.

All written code is simple and easy to read and understand.
Documentation is __NOT__ provided except for some parts.

## namespace `mvvmStandard.ViewModel`
  
__ViewModelBase__ is an `abstract class` that implements three interfaces:

- INotifyPropertyChanged

- INotifyDataError

- IDataError

When designing _ViewModel_, use `SetProperty` method to change a property value and invoke at least the `event PropertyChanged`.

There are extensions that does apply validation rule to set property.

__ViewModelBasing__ is another `abstract class` that inherits __ViewModelBase__, and implements __INotifyPropertyChanging__.

## namespace `mvvmStandard.Commands`

This namespace holds different implementations for `ICommand` interface.

ICommand requires two __actions__: _Execute_ and _CanExecute_, with one `object` parameter.

By using commands package, now there is an ability to use __function__ execute that stores its value to `Result`.

The good thing about CommandFunction is that it implements `INotifyPropertyChanged` on Result, so binding the result can be done directly to the UI.

```csharp
CommandFunction< int > cmd = new  CommandFunction < int > (() => { return  5; }, null);

cmd.PropertyChanged += (x,y) => { OnPropertyChanged("cmdResult"); };

int  cmdResult => cmd.Result;
```

There is also an ability to define the types of parameter that should go into Executing or CanExecute.

Look at command types below:

- Action:
  - CommandAction
  - CommandAction< TExecParam, TCanExecParam >
  - CommandActionAsync *(`async` on __EXECUTE__ only)*.
  - CommandActionAsync< TExecParam, TCanExecParam > *(`async` on __EXECUTE__ only)*.

- Function:
  - CommandFunction< TResult >
  - CommandFunction< TExecParam, TCanExecParam, TResult >
  - CommandFunctionAsync< TResult > *(`async` on __EXECUTE__ only)*.
  - CommandFunctionAsync< TExecParam, TCanExecParam, TResult > *(`async` on __EXECUTE__ only)*.

__Async commands__ also implements INotifyPropertyChanged in order to check and test tasks by reading `TaskStatus Status` of the command.

```csharp
CommandActionAsync  cmd = new  CommandActionAsync(() => { }, null);

cmd.PropertyChanged += (x, y) =>
{
    if (y.PropertyName == "Status")
    {
        switch (cmd.Status)
        {
            case  TaskStatus.Canceled:
            break;
            case  TaskStatus.Created:
            break;
            case  TaskStatus.Faulted:
            break;
            case  TaskStatus.RanToCompletion:
            break;
            case  TaskStatus.Running:
            break;
            case  TaskStatus.WaitingForActivation:
            break;
            case  TaskStatus.WaitingForChildrenToComplete:
            break;
            case  TaskStatus.WaitingToRun:
            break;
            default:
            break;
        }
    }
};
```

## namespace `mvvmStandard.Validation`

Data validation has become easy with namespace `mvvmStandard.Validation`.

Validation can be done with static or instance of `ErrorValidator` or `WarningValidator`,

or by creating an instance of `PropValidator`. In addition, any validator can be assigned in `SetProperty`.

- [x] Error/Warning Validator.
- [x] Property Validator.
- [ ] Class Validator.

## CRUDObservableCollection

Maybe this is the best part of this library. Normal _ObservableCollection_ is only a Collection that is bind-able to UI.

__CRUDObservableCollection__ is an ObservableCollection that has one extra feature, Custom **CRUD** using `async`:
  
- `Func< IEnumerable< T > > CustomSelectFunc`: to define the way of selecting data. (uses __await__ internally)

- `Func< int, int, IEnumerable< T > > CustomPagingSelectFunc`: selecting data with paging. (uses __await__ internally)

- `Action< IEnumerable< T > > CustomSaveAddAct`: to deal with new inserted data;

- `Action< IEnumerable< T > > CustomSaveEditAct`: to deal with edited data;

- `Action< IEnumerable< T > > CustomSaveDeleteAct`: to deal with removed data;

The best practice to deal with this collection is inhertance.

## Snippets

If this library is to make you type less, then it would be useless without a snippet package that helps you to type faster.
Repeating code is boring and might become confusing, so using these snippets help to focus while coding.  

-To make this package identical, all snippets begin with --vm--.-

## Notes

This library may have issue regarding CancellationToken in Async commands (not tested).

This is my first library  and publish on github. If you face issues or have suggestions, I will try responding to them, and I hope you help me in that too.
