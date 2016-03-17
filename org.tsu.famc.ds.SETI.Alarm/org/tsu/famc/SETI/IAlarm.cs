using System.ServiceModel;

namespace org.tsu.famc.ds.SETI
{
    [ServiceContract(SessionMode = SessionMode.Required,
                 CallbackContract = typeof(IAlarmCallback))]
    public interface IAlarm
    {
        [OperationContract(IsOneWay = true)]
        void Subscribe(Target target);
    }

    public interface IAlarmCallback
    {
        [OperationContract(IsOneWay = true)]
        void Alarm(int[] data);
    }


}