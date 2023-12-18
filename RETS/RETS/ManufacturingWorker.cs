
namespace RETS
{
    public class ManufacturingWorker : WorkerBase
    {
        public ManufacturingWorker(string intime, string outtime) : base(intime, outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }

        public string Intime { get; private set; }
        public string Outtime { get; private set; }

        public TimeSpan Difference { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan Doba { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddCalculated24h(DateTime newTime1, DateTime newTime2)
        {
            throw new NotImplementedException();
        }

        public void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {
            throw new NotImplementedException();
        }

        public void EveryDaySummary()
        {
            throw new NotImplementedException();
        }

        public Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }

        public void PerformActionsBasedOnWorkedHourszwykły(TimeSpan totalWorkHoursInMonth, TimeSpan totalWorkHours)
        {
            throw new NotImplementedException();
        }

    }
}
