namespace org.tsu.famc.ds.SETI
{
    public enum Target
    {
        NewPlanet,
        NewLife,
        Aliens
    };

    public delegate void Notify();

    public interface ISETI
    {
        void AnalyzeData(int[] data);
        void AddSubscriber(int key, Notify obserer, Target target);
        void DeleteSubscriber(int key);
    }
}