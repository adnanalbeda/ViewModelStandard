namespace ViewModelStandard.Interfaces.Commands
{
    using System.Threading.Tasks;
    
    public interface ICommandFunctionAsync<TResult> 
        : ICommandFunction<TResult>, ICommandAsyncBase
    {
        Task<TResult> ExecuteAsync();
    }

}

