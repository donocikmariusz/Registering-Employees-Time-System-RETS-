namespace RETS
{
    public class HybridWorker : WorkerBase, IRets
    {
        public event TimeAddedDelegate TimeAdded;
        public HybridWorker(string intime, string outtime) : base(intime, outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }
        public string Intime { get; private set; }
        public string Outtime { get; private set; }

        public TimeSpan Difference { get; private set; }
        public TimeSpan Day { get; private set; }

        public override void AddCalculated24h(DateTime newTime1, DateTime newTime2)
        {
            Day = (TimeSpan.FromHours(24) - (newTime1 - newTime2));
            EveryDayResult.Add(Day);
            this.OnTimeAdded();
        }
        public override void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {
            Difference = newTime2 - newTime1;
            EveryDayResult.Add(Difference);
            this.OnTimeAdded();
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();

            foreach (TimeSpan totalWorkedTime in EveryDayResult)
            {
                statistics.TotalWorkedTime += totalWorkedTime;
            }

            return statistics;
        }
    }
}
