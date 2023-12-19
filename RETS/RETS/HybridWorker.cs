
namespace RETS
{
    public class HybridWorker : WorkerBase
    {
        public HybridWorker(string intime, string outtime) : base(intime, outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }
        public string Intime { get; private set; }
        public string Outtime { get; private set; }

        public override void AddCalculated24h(DateTime newTime1, DateTime newTime2)
        {
            throw new NotImplementedException();
        }
        public override void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {
            throw new NotImplementedException();
        }
        public override void EveryDaySummary()
        {
            throw new NotImplementedException();
        }
        public override Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }
        public override void ShowStatistics()
        {
            throw new NotImplementedException();
        }


        
    }
}
