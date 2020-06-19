
# ViewModelStandard Library

ViewModel for every platform.

MIT License - All rights reserved.

## About

ViewModelStandard, as the name hint, is a standard library that can be used on every .net platform:

- .net Framework.
- .net Core.
- Xamarin.

Its main goal is to help in designing __ViewModel__, with a package of:

- [x] _ViewModelBase_
- [x] _Commands_
- [x] _Validators_
- [x] _Snippets_

## namespace `ViewModelStandard`
  
__ViewModelBase__ is an `abstract class` implemnting three interfaces:

- INotifyPropertyChanged

- INotifyDataError

- IDataError

When designing _ViewModel_, use `SetProperty` method to change a property value and invoke at least the `event PropertyChanged`.*

There are extensions that does apply validation rule to set property.

__ViewModelBasing__ is another `abstract class` that inherits __ViewModelBase__, and implements __INotifyPropertyChanging__.

## namespace `ViewModelStandard.Commands`

This namespace holds different implementations for `ICommand` interface.

ICommand requires two __actions__: _Execute_ and _CanExecute_, with one `object` parameter.

By using commands package, now there is an ability to use __function__ execute that stores its value to `Result`.

The good thing about CommandFunction is that it implements `INotifyPropertyChanged` on Result, so binding the result can be done directly to the UI.

C# declaration:

```csharp
CommandFunction<int> cmd { get; private set; }
    = new  CommandFunction<int> (() => { return  5; }, null);
```

XAML Binding:

```XAML
<TextBlock Text={Binding cmd.Result} .../>
```

There is also an ability to define the types of parameter that should go into Execute and CanExecute.

Look at command types below:

- Action:
  - CommandAction
  - CommandAction< T >
  - CommandActionAsync
  - CommandActionAsync< T >

- Function:
  - CommandFunction< TResult >
  - CommandFunction< T, TResult >
  - CommandFunctionAsync< TResult >
  - CommandFunctionAsync< T, TResult >

__Async commands__ implements INotifyPropertyChanged in order to check and test tasks by reading `TaskStatus Status` of the command.

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
            default:
            break;
        }
    }
};
```

## namespace `ViewModelStandard.Validation`

Data validation has become easy with namespace `ViewModelStandard.Validation`.

Validation can be done with static or instance of `ErrorValidator` or `WarningValidator`,

or by creating an instance of `PropValidator`. In addition, any validator can be assigned in `SetProperty`.

- [x] Error/Warning Validator.
- [x] Property Validator.
- [ ] Class Validator.

## Snippets

If this library is to make you code more and type more, then it would be useless without a snippet package that helps you to type faster.
Repeating code is boring and might become confusing, so using these snippets help to focus while coding.  

- These snippets begin with **vm**.
