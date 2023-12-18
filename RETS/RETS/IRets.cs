
namespace RETS
{
    public interface IRets
    {
        string Intime { get; }
        string Outtime { get; }
        void AddTimeDifference(DateTime newTime1, DateTime newTime2);
        void AddCalculated24h(DateTime newTime1, DateTime newTime2);
        void EveryDaySummary();
        Statistics GetStatistics();
        void PerformActionsBasedOnWorkedHourszwykły(TimeSpan totalWorkHoursInMonth, TimeSpan totalWorkHours);
    }
}
