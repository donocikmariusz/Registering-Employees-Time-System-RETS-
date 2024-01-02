namespace RETS
{
    public delegate void TimeAddedDelegate(object sender, EventArgs args);
    public interface IRets
    {
        string Intime { get; }
        string Outtime { get; }
        public TimeSpan Difference { get; }
        public TimeSpan Day { get; }
        void AddTimeDifference(DateTime newTime1, DateTime newTime2);
        void AddCalculated24h(DateTime newTime1, DateTime newTime2);
        void EveryDaySummary();
        Statistics GetStatistics();
        void ShowStatistics();

        event TimeAddedDelegate TimeAdded;
    }
}
