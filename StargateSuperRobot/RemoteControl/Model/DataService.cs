using System.Threading.Tasks;

namespace RemoteControl.Model
{
    public class DataService : IDataService
    {
        public Task<DataItem> GetData()
        {
            // Use this to connect to the actual data service

            // Simulate by returning a DataItem
            var item = new DataItem("Welcome to Star Gate");
            return Task.FromResult(item);
        }
    }
}