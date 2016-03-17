namespace org.tsu.famc.ds.SETI
{
    public delegate void Notify(int [] data);  

    public interface ISETI
    {
        void AnalyzeData(int[] data);
        int AddSubscriber(Notify obserer, Target target);
        void DeleteSubscriber(int key);
    }
}