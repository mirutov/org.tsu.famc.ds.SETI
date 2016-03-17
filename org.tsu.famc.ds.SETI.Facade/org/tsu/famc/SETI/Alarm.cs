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
            try
            {
                Console.WriteLine("send");
                callback.Alarm(data);
                Console.WriteLine("ok");
            }
            catch (Exception e)
            {
                Console.WriteLine("Alarm.Ring Cannot send a signal to " + key);
                Console.WriteLine(e.Message);
                SETIProxy.GetSETI().DeleteSubscriber(key);
            }
        }

        public int Subscribe(Target target)
        {
            callback = OperationContext.Current.GetCallbackChannel<IAlarmCallback>();
            key = SETIProxy.GetSETI().AddSubscriber(Ring, target);
            return key;
        }
    }
}
