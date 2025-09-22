using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.UtillsClasses
{
    public class TaskUtills
    {
        //Потому что в 4.0 нет таск делай
        public static Task Delay(double milliseconds)
        {
            var tcs = new TaskCompletionSource<bool>();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (obj, args) =>
            {
                tcs.TrySetResult(true);
            };
            timer.Interval = milliseconds;
            timer.AutoReset = false;
            timer.Start();
            return tcs.Task;
        }
    }
}
