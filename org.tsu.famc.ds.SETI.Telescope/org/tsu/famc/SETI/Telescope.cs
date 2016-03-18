using System;
using System.ServiceModel;
using org.tsu.famc.ds.SETI.Telescope.Proxy;

namespace org.tsu.famc.ds.SETI
{
    class Hubble
    {
        static void Main(string[] args)
        {
            int[] data1 = new int[3] { 1, 1, 1 };
            int[] data2 = new int[3] { 2, 2, 2 };
            int[] data3 = new int[3] { 3, 3, 3 };

            ReceiverClient rc = new ReceiverClient();
            try {                
                rc.Receive(data1);
                rc.Receive(data2);
                rc.Receive(data3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

