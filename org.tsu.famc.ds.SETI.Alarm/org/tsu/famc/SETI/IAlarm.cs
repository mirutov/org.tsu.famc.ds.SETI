using System.ServiceModel;

namespace org.tsu.famc.ds.SETI
{
    [ServiceContract(SessionMode = SessionMode.Required,
                 CallbackContract = typeof(IAlarmCallback))]
    public interface IAlarm
    {
        [OperationContract]
        int Subscribe(Target target);
    }

    public interface IAlarmCallback
    {
        [OperationContract]
        void Alarm(int[] data);
    }


}