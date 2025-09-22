using System.Threading;

namespace Updater
{
    public class MutexHelper
    {
        private Mutex _appMutex;
        private string name;


        public MutexHelper(string mut)
        {
            this.name = mut;
        }

        public bool CreateMutex()
        {
            bool createdNew;
            _appMutex = new Mutex(false, name, out createdNew);
            return createdNew;
        }

        public void CloseMutex()
        {
            if (_appMutex != null)
            {
                _appMutex.Close();
                _appMutex = null;
            }
        }
    }
}
