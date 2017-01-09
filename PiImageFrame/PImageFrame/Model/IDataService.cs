using System.Threading.Tasks;

namespace PImageFrame.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}