namespace RETS
{
    public class TimeValidator
    {
        public static bool IsValidTime(string intime)
        {

            // Podział wprowadzonego czasu na godziny i minuty
            string[] parts = intime.Split(':');

            if (parts.Length == 2)
            {
                // Sprawdzenie, czy godziny i minuty są liczbami całkowitymi w odpowiednich zakresach
                if (int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
                {
                    return hours >= 0 && hours < 24 && minutes >= 0 && minutes < 60;

                }
            }
            return false;

        }

    }
}