using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.tsu.famc.ds.SETI.Scout.Proxy;
using System.ServiceModel;

namespace org.tsu.famc.ds.SETI
{
    public class SpaceScout : IAlarmCallback
    {
        Target target;
        int key;

        public SpaceScout(Target argTarget)
        {
            target = argTarget;
            AlarmClient ac = new AlarmClient(new InstanceContext(this));
            key = ac.Subscribe(target);
        }

        public void Alarm(int[] data)
        {
            Console.Write("SpaceScout-" + target +".Alarm (key=" + key + ")");
            foreach (int i in data)
            {
                Console.Write(" " + i);
            }
            Console.WriteLine("");
        }
    }

    class ScoutGroup
    {
        static void Main(string[] args)
        {
            
            SpaceScout lifeScout = new SpaceScout(Target.Life);
            SpaceScout alienScout = new SpaceScout(Target.Alien);
            SpaceScout planetScout = new SpaceScout(Target.Planet);
            Console.ReadLine();
        }
    }
}
