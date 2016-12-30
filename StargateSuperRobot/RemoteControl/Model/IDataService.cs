using System.Threading.Tasks;

namespace RemoteControl.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}