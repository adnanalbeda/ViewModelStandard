namespace ViewModelStandard.Interfaces.Commands
{
    using System.Threading.Tasks;

    public interface ICommandActionAsync<T> 
        : ICommandAction<T>, ICommandAsyncBase
    {
        Task ExecuteAsync(T parameter);
    }
}

