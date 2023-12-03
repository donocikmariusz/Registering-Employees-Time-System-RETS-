namespace RETS
{
    public class TimeCalculator : System.Object
    {
        public static TimeSpan SumTimeSpans(List<TimeSpan> timeSpans)
        {
            TimeSpan total = TimeSpan.Zero;

            foreach (var timeSpan in timeSpans)
            {
                total = total.Add(timeSpan);
            }
            return total;
        }

        public static string FormatTotalTime(TimeSpan total)
        {
            return $"{total.TotalHours:N0} godzin {total.Minutes} minut";
        }
    }

  }
