namespace RETS
{
    public class HybridWorker : WorkerBase, IRets
    {
        private readonly TimeSpan eightHours = TimeSpan.FromHours(8);
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

        public override void EveryDaySummary()
        {
            Console.WriteLine("Every day statistics:");
            Console.WriteLine("");

            for (int i = 0; i < EveryDayResult.Count; i++)
            {
                if (eightHours < EveryDayResult[i])
                {
                    TimeSpan overtime = EveryDayResult[i] - eightHours;
                    Console.WriteLine($"Day {i + 1} was {Math.Abs(EveryDayResult[i].Hours):D2} hours {Math.Abs(EveryDayResult[i].Minutes):D2} minutes - overtime value {overtime.Hours} hours {overtime.Minutes} minutes");
                }
                else if (eightHours > EveryDayResult[i])
                {
                    TimeSpan undertime = eightHours - EveryDayResult[i];
                    Console.WriteLine($"Day {i + 1} was {Math.Abs(EveryDayResult[i].Hours):D2} hours {Math.Abs(EveryDayResult[i].Minutes):D2} minut - undertime wynosi: {undertime.Hours} hours {undertime.Minutes} minutes");
                }
                else
                {
                    Console.WriteLine($"Day {i + 1} was {Math.Abs(EveryDayResult[i].Hours):D2} hours {Math.Abs(EveryDayResult[i].Minutes):D2} it is accurate down to the minute");
                }
            }
        }
    }
}
