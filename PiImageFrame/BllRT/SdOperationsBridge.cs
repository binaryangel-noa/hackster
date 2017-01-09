using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace BllRT
{
    public sealed class SdOperationsBridge
    {
        SdOperations SdOperations = new SdOperations();
        public IAsyncOperation<IEnumerable<string>> ListFiles()
        {
            return SdOperations.ListFiles(true).AsAsyncOperation();
        }

        public IAsyncOperation<IEnumerable<string>> ListCopiedFiles()
        {
            return SdOperations.ListCopiedFiles(true).AsAsyncOperation();
        }

        public IAsyncOperation<bool> CopyFiles()
        {
            return SdOperations.CopyFiles().AsAsyncOperation();
        }

    }
}
