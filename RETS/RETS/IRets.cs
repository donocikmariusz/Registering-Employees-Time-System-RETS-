﻿
using static RETS.WorkerBase;

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
        void ShowStatistics();
        event TimeAddedDelegate TimeAdded;
    }
}
