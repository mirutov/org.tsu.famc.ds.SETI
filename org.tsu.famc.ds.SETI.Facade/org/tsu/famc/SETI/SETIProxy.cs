using Ninject;
using System;
using System.IO;

namespace org.tsu.famc.ds.SETI
{
    internal class SETIProxy
    {
        private static object locker = new object();
        private static ISETI seti = null;
        internal static ISETI GetSETI()
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
