using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace org.tsu.famc.ds.SETI
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Alarm : IAlarm
    {
        IAlarmCallback callback = null;
        IContextChannel channel = null;
        string sessionId;
        void Ring(int[] data)
        {
            if (channel.State == CommunicationState.Opened)
            {
                callback.Alarm(data);
            }
            else
            {
                SETIProxy.GetSETI().DeleteSubscriber(GetHashCode());
            }
        }

        public void Subscribe(Target target)
        {
            callback = OperationContext.Current.GetCallbackChannel<IAlarmCallback>();
            channel = OperationContext.Current.Channel;
            sessionId = channel.SessionId;
            SETIProxy.GetSETI().AddSubscriber(GetHashCode(), Ring, target);
        }
    }
}
