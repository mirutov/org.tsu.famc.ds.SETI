using System;
using System.Threading;
using System.Collections.Generic;


namespace org.tsu.famc.ds.SETI
{

    public class DeepThought : ISETI
    {
        int subscribersCount;
        private Dictionary<int, Notify> lifeObservers;
        private Dictionary<int, Notify> planetObservers;
        private Dictionary<int, Notify> alienObservers;
        private List<int[]> lifeMessages;
        private List<int[]> planetMessages;
        private List<int[]> alienMessages;

        private string GetArrayAsString(int[] data)
        {
            string ret = "{";
            int i = 0;
            for (; i<data.Length-1; i++)
            {
                ret = ret + data[i] + ", ";
            }
            ret = ret + data[i] + "}";
            return ret;
        }

        private Dictionary<int, Notify> GetObservers(Target target)
        {
            switch (target)
            {
                case Target.Planet:
                    return planetObservers;
                case Target.Life:
                    return lifeObservers;
            }
            return alienObservers;
        }

        private List<int[]> GetMessages(Target target)
        {
            switch (target)
            {
                case Target.Planet:
                    return planetMessages;
                case Target.Life:
                    return lifeMessages;
            }
            return alienMessages;
        }


        private void SaveData(Target target, int[] data)
        {
            List<int[]> messages = GetMessages(target);
            lock (messages)
            {
                messages.Add(data);
            }
        }

        private void NotifyObservers(Target target)
        {
            Dictionary<int, Notify> observers = GetObservers(target);
            if (observers.Count == 0)
            {
                return;
            }
            lock (observers)
            {
                List<int[]> messages = GetMessages(target);
                lock (messages)
                {
                    foreach (KeyValuePair<int, Notify> observer in observers)
                    {
                        foreach (int[] message in messages)
                        {
                            observer.Value(message);
                        }
                    }
                    messages.Clear();
                }
            }
        }

        Target FindSomething(int[] data)
        {
            System.Random random = new System.Random();
            int randValue = random.Next(System.Enum.GetValues(typeof(Target)).Length);
            Target result = (Target)System.Enum.ToObject(typeof(Target), randValue);            
            Thread.Sleep(random.Next(5000));
            Console.WriteLine("DeepThought.FindSomething: " + result + " was found at " + GetArrayAsString(data));
            return result;
        }

        public DeepThought()
        {
            subscribersCount = 0;
            lifeObservers = new Dictionary<int, Notify>();
            planetObservers = new Dictionary<int, Notify>();
            alienObservers = new Dictionary<int, Notify>();
            lifeMessages = new List<int[]>();
            planetMessages = new List<int[]>();
            alienMessages = new List<int[]>();
        }

        public void AnalyzeData(int[] data)
        {            
            Target target = FindSomething(data);
            SaveData(target, data);
            NotifyObservers(target);
        }

        public int AddSubscriber(Notify observer, Target target)
        {
            Dictionary<int, Notify> observers = GetObservers(target);
            lock (observers)
            {
                subscribersCount++;
                observers.Add(subscribersCount, observer);
                Console.WriteLine("Observer " + subscribersCount + "(" + target + ") was connected");
            }
            return subscribersCount;
        }

        public void DeleteSubscriber(int key)
        {            
            if (planetObservers.ContainsKey(key))
            {                
                lock (planetObservers)
                {
                    planetObservers.Remove(key);
                    Console.WriteLine("Observer " + key + " was disconnected");
                }
                return;
            }
            if (lifeObservers.ContainsKey(key))
            {                
                lock (lifeObservers)
                {
                    lifeObservers.Remove(key);
                    Console.WriteLine("Observer " + key + " was disconnected");
                }
                return;
            }
            if (alienObservers.ContainsKey(key))
            {
                lock (alienObservers)
                {
                    alienObservers.Remove(key);
                    Console.WriteLine("Observer " + key + " was disconnected");
                }
            }            
        }
    }
}