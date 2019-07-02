namespace ViewModelStandard.Interfaces.Commands
{

    using System.Threading.Tasks;

    public interface ICommandActionAsync 
        : ICommandAction, ICommandAsyncBase
    {
        Task ExecuteAsync();
    }

}

