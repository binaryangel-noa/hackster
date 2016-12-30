using GalaSoft.MvvmLight;
using RemoteControl.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.ViewModel
{
    public class MessageReceiverViewModel : ViewModelBase
    {
        public MessageReceiverViewModel()
        {
            MessageList = new ObservableCollection<DataItem>();
        }
        private ObservableCollection<DataItem> mMessageList;

        public ObservableCollection<DataItem> MessageList
        {
            get { return mMessageList; }
            set
            {
                Set(ref mMessageList, value);
            }
        }


    }
}
