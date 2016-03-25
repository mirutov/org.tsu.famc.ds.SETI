using Ninject;
using System;

namespace org.tsu.famc.ds.SETI
{
    public class SETIProxy
    {
        private static object locker = new object();
        private static ISETI seti = null;
        public static ISETI GetSETI()
        {
            lock (locker)
            {
                if (seti == null)
                {
                    try
                    {
                        var kernel = new Ninject.StandardKernel();
                        kernel.Load("SETI.xml");
                        seti = kernel.Get<ISETI>();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                return seti;
            }
        }
    }
}
