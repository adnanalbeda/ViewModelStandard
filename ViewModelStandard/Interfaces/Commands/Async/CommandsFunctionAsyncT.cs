namespace ViewModelStandard.Interfaces.Commands
{
    using System.Threading.Tasks;
    
    public interface ICommandFunctionAsync<T, TResult> 
        : ICommandFunction<T, TResult>, ICommandAsyncBase
    {
        Task<TResult> ExecuteAsync(T parameter);
    }

}

