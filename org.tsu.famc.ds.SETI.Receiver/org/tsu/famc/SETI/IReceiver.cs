using System.ServiceModel;

namespace org.tsu.famc.ds.SETI
{
    [ServiceContract]
    public interface IReceiver
    {
        [OperationContract]
        void Receive(int[] data);
    }
}