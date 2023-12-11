namespace RETS
{
    public class TimeCalculator
    {
        public static TimeSpan SumTimeSpans(List<TimeSpan> result)
        {
            TimeSpan total = TimeSpan.Zero;

            foreach (var timeSpan in result)
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
