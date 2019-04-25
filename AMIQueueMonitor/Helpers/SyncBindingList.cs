using System.ComponentModel;

namespace AMIQueueMonitor.Helpers

{
    public class SyncBindingList<T> : BindingList<T>
    {
        private ISynchronizeInvoke _syncObject;
        public SyncBindingList(ISynchronizeInvoke syncObject)
        {
            _syncObject = syncObject;
        }

        delegate void ListChangedDelegate(ListChangedEventArgs e);

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (_syncObject.InvokeRequired)
            {
                ListChangedDelegate d = new ListChangedDelegate(base.OnListChanged);
                _syncObject.BeginInvoke(d, new object[] { e });
            }
            else
            {
                base.OnListChanged(e);
            }
        }
    }
    
}
