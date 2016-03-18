using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace org.tsu.famc.ds.SETI
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Alarm : IAlarm
    {
        IAlarmCallback callback = null;
        int key;
        public void Ring(int[] data)
        {
            Task.Run(() =>
            {
                try
                {
                    callback.Alarm(data);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    SETIProxy.GetSETI().DeleteSubscriber(key);
                }
            });
        }

        public int Subscribe(Target target)
        {
            callback = OperationContext.Current.GetCallbackChannel<IAlarmCallback>();
            key = SETIProxy.GetSETI().AddSubscriber(Ring, target);
            return key;
        }
    }
}
