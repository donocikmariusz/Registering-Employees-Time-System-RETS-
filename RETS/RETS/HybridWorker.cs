
namespace RETS
{
    public class HybridWorker : WorkerBase
    {
        public HybridWorker(string intime, string outtime) : base(intime, outtime)
        {

        }

        public string Intime => throw new NotImplementedException();

        public string Outtime => throw new NotImplementedException();

      
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
            double hoursWorked = totalWorkHours.TotalHours;
            double totalMonthlyHours = totalWorkHoursInMonth.TotalHours;

            switch (hoursWorked)
            {
                case double hours when hours >= 0 && hours < 10:
                    Console.WriteLine($"Przegina bardzo");
                    break;
                case double hours when hours >= 10 && hours < 100:
                    Console.WriteLine($"Norma");
                    break;
                case double hours when hours >= 10 && hours < 100:
                    Console.WriteLine($"Nie musi a chodzi do firmy - dziwny gość, pewnie nie ma rodziny tylko kota.");
                    break;
                default:
                    throw new Exception("Coś poszło nie tak...");

            }
        }
    }
}
