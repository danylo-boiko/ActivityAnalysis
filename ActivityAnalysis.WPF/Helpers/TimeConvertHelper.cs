using System;

namespace ActivityAnalysis.WPF.Helpers
{
    public static class TimeConvertHelper
    {
        public static string SecondsToAbbreviatedString(this TimeSpan time, int accuracy = 1)
        {
            double totalSeconds = time.TotalSeconds;
            switch (totalSeconds)
            {
                case <=60:
                    return $"{Math.Round(totalSeconds, accuracy)} sec";
                case <=3600:
                    return $"{Math.Round(totalSeconds / 60, accuracy)} min";
                default:
                    return $"{Math.Round(totalSeconds / 3600, accuracy)} hr";
            }
        }
        
        public static string SecondsToFullString(this TimeSpan time, int accuracy = 1)
        {
            double totalSeconds = time.TotalSeconds;
            switch (totalSeconds)
            {
                case <=60:
                    return $"{Math.Round(totalSeconds, accuracy)} seconds";
                case <=3600:
                    return $"{Math.Round(totalSeconds / 60, accuracy)} minutes";
                default:
                    return $"{Math.Round(totalSeconds / 3600, accuracy)} hours";
            }
        }
        
        public static string SecondsToAbbreviatedString(this double seconds, int accuracy = 1)
        {
            switch (seconds)
            {
                case <=60:
                    return $"{Math.Round(seconds, accuracy)} sec";
                case <=3600:
                    return $"{Math.Round(seconds / 60, accuracy)} min";
                default:
                    return $"{Math.Round(seconds / 3600, accuracy)} hr";
            }
        }
        
        public static string SecondsToFullString(this double seconds, int accuracy = 1)
        {
            switch (seconds)
            {
                case <=60:
                    return $"{Math.Round(seconds, accuracy)} seconds";
                case <=3600:
                    return $"{Math.Round(seconds / 60, accuracy)} minutes";
                default:
                    return $"{Math.Round(seconds / 3600, accuracy)} hours";
            }
        }
    }
}