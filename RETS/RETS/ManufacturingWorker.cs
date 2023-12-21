
using System;

namespace RETS
{
    public class ManufacturingWorker : WorkerBase, IRets
    {
        private const string fileName = "times.txt";

        private readonly TimeSpan eightHours = TimeSpan.FromHours(8);
        public ManufacturingWorker(string intime, string outtime) : base(intime, outtime)
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
            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine($"{Day.Hours}:{Day.Minutes:D2}");
            }
        }
        public override void AddTimeDifference(DateTime newTime1, DateTime newTime2)
        {
            Difference = newTime2 - newTime1;
            EveryDayResult.Add(Difference);
            this.OnTimeAdded();

            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine($"{Difference.Hours}:{Difference.Minutes:D2}");
            }
        }

        public override Statistics GetStatistics()
        {
            var timesFromFile = this.ReadTimesFromFile();
            var result = this.CountStatistics(timesFromFile);
            return result;
        }

        private List<TimeSpan> ReadTimesFromFile()
        {
            var times = new List<TimeSpan>();

            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var parts = line.Split(':');
                        if (parts.Length == 2 && int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
                        {
                            var timeSpan = new TimeSpan(hours, minutes, 0);
                            times.Add(timeSpan);
                        }
                        line = reader.ReadLine();
                    }
                }
            }
            return times;
        }

        public Statistics CountStatistics(List<TimeSpan> times)
        {
            var statistics = new Statistics();

            foreach (TimeSpan totalWorkedTime in times)
            {
                statistics.TotalWorkedTime += totalWorkedTime;
            }

            return statistics;
        }

        public override void EveryDaySummary()
        {
            var timesFromFile = this.ReadTimesFromFile();
            Console.WriteLine("Every day statistics:");
            Console.WriteLine("");
            for (int i = 0; i < timesFromFile.Count; i++)
            {
                if (eightHours < timesFromFile[i])
                {
                    TimeSpan overtime = timesFromFile[i] - eightHours;
                    Console.WriteLine($"Day {i + 1} was {Math.Abs(timesFromFile[i].Hours):D2} hours {Math.Abs(timesFromFile[i].Minutes):D2} minutes - overtime is {overtime.Hours} hours {overtime.Minutes} minutes");
                }
                else if (eightHours > timesFromFile[i])
                {
                    TimeSpan undertime = eightHours - timesFromFile[i];
                    Console.WriteLine($"Day {i + 1} was {Math.Abs(timesFromFile[i].Hours):D2} hours {Math.Abs(timesFromFile[i].Minutes):D2} minut - undertime is: {undertime.Hours} hours {undertime.Minutes} minutes");
                }
                else
                {
                    Console.WriteLine($"Day {i + 1} was {Math.Abs(timesFromFile[i].Hours):D2} hours {Math.Abs(timesFromFile[i].Minutes):D2} - it is accurate down to the minute");
                }
            }


        }
    }
}
