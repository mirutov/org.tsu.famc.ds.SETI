using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace org.tsu.famc.ds.SETI
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Receiver : IReceiver
    {
        private int count;

        private Task AnalyzeData(int[] data)
        {
            return Task.Run(() =>
            {
                ISETI seti = SETIProxy.GetSETI();
                seti.AnalyzeData(data);
            });
        }

        public Receiver()
        {
            count = 0;
        }


        public async void Receive(int[] data)
        {
            Console.WriteLine("Receiver.Receive: " + ++count);           
            await AnalyzeData(data);
        }
    }
}
