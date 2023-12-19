namespace RETS
{
    public abstract class WorkerBase : IRets
    {
        public delegate void TimeAddedDelegate(object sender, EventArgs args);
        public abstract event TimeAddedDelegate TimeAdded;
        public WorkerBase(string intime, string outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }
        public string Intime { get; private set; }
        public string Outtime { get; private set; }

        public abstract void AddTimeDifference(DateTime newTime1, DateTime newTime2);
        public abstract void AddCalculated24h(DateTime newTime1, DateTime newTime2);
        public abstract void ShowStatistics();
        public abstract void EveryDaySummary();
        public abstract Statistics GetStatistics();


    }
}
