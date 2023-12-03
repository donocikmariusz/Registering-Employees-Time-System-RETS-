

namespace RETS
{
    public class CalculateTime
    {
        public static TimeSpan TotalTimeForOneDay(string intime, string outtime, int ktorydzien, out TimeSpan difference)
        {
            string[] formats = { "H:mm", "HH:mm" };
            DateTime.TryParseExact(intime, formats, null, System.Globalization.DateTimeStyles.None, out DateTime time1);
            DateTime.TryParseExact(outtime, formats, null, System.Globalization.DateTimeStyles.None, out DateTime time2);
            difference = time2 - time1;
            Console.WriteLine($"Dnia {ktorydzien} parch był {Math.Abs(difference.Hours):D2} hour {Math.Abs(difference.Minutes):D2} minutes");
            return difference;
        }

        public static TimeSpan SumTimeSpans(List<TimeSpan> timeSpans)
        {
            TimeSpan total = TimeSpan.Zero;

            foreach (var timeSpan in timeSpans)
            {
                total = total.Add(timeSpan);
            }

            return total;
        }
    }
}












