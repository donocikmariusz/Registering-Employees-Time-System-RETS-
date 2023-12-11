namespace RETS
{
    public class CalculateTime
    {
        public List<TimeSpan> everyDayResult = new List<TimeSpan>();

        public CalculateTime(string intime, string outtime)
        {
            this.Intime = intime;
            this.Outtime = outtime;
        }

        private string Intime { get; set; }
        private string Outtime { get; set; }

        public TimeSpan EveryDayResult
        {
            get
            {
                return everyDayResult.Count > 0
            ? everyDayResult.Aggregate((acc, ts) => acc.Add(ts))
            : TimeSpan.Zero;

            }
        }

        public string FormattedEveryDayResult
        {
            get
            {
                var totalResult = EveryDayResult;
                int totalHours = (int)totalResult.TotalHours;
                int minutes = totalResult.Minutes;

                string formattedResult = $"{totalHours} godzin, {minutes} minut";
                return formattedResult;
            }
        }

        public TimeSpan Difference { get; private set; }
        public TimeSpan Doba { get; private set; }

        public void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {

            Difference = newTime2 - newTime1;
            everyDayResult.Add(Difference);
        }

        public static TimeSpan SumTimeSpans(List<TimeSpan> result)
        {
            return result.Aggregate(TimeSpan.Zero, (acc, ts) => acc.Add(ts));
        }

        public static string FormatTotalTime(TimeSpan total)
        {
            return $"{total.TotalHours:N0} godzin {total.Minutes} minut";
        }

        public void AddCalculated24h(DateTime newTime1, DateTime newTime2)
        {
            TimeSpan doba = TimeSpan.FromHours(24);

            Doba = (TimeSpan.FromHours(24) - (newTime1 - newTime2));
            everyDayResult.Add(Doba);
        }
    }
}
