namespace RETS
{
    public class CalculateTime
    {
        public List<TimeSpan> everyDayResult = new List<TimeSpan>();

        public CalculateTime(string intime, string outtime)
        {
            this.Intime = ParseTime(intime);
            this.Outtime = ParseTime(outtime);
        }

        private DateTime Intime { get; set; }
        private DateTime Outtime { get; set; }

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

        public void AddTimeDifference(string newIntime, string newOuttime)
        {
            DateTime newTime1 = ParseTime(newIntime);
            DateTime newTime2 = ParseTime(newOuttime);

            Difference = newTime2 - newTime1;
            everyDayResult.Add(Difference);

            this.Intime = newTime1;
            this.Outtime = newTime2;
        }

        public static TimeSpan SumTimeSpans(List<TimeSpan> result)
        {
            return result.Aggregate(TimeSpan.Zero, (acc, ts) => acc.Add(ts));
        }

        private DateTime ParseTime(string timeString)
        {
            string[] formats = { "H:mm", "HH:mm", "H:m", "HH:m" };
            DateTime.TryParseExact(timeString, formats, null, System.Globalization.DateTimeStyles.None, out DateTime time);
            return time;
        }


    }
}
