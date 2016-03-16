
using System.Collections.Generic;

namespace org.tsu.famc.ds.SETI
{
    internal struct Observer
    {
        public Target target;
        public Notify callback;

        public Observer(Target argTarget, Notify argCallback)
        {
            target = argTarget;
            callback = argCallback;
        }
    }


    public class SETI : ISETI
    {
        Dictionary<int, Observer> observers;

        public SETI()
        {
            observers = new Dictionary<int, Observer>();
        }

        public void AnalyzeData(int[] data)
        {
            System.Random random = new System.Random();
            int randValue = random.Next(System.Enum.GetValues(typeof(Target)).Length);
            Target result = (Target)System.Enum.ToObject(typeof(Target), randValue);
            foreach (KeyValuePair<int, Observer> i in observers)
            {
                if (i.Value.target == result)
                {
                    i.Value.callback();
                }                
            }
        }

        public void AddSubscriber(int key, Notify observer, Target target)
        {
            lock (observers)
            {
                observers.Add(key, new Observer(target, observer));
            }
        }

        public void DeleteSubscriber(int key)
        {
            lock (observers)
            {
                observers.Remove(key);
            }
        }
    }
}