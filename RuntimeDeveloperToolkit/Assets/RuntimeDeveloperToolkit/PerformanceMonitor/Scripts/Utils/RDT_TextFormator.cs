namespace RuntimePerformanceMonitor
{
    public static class RDT_TextFormator
    {
        public static string GetFormatedString(int value)
        {
            if (value >= 1_000_000_000)
                return $"{value / 1_000_000_000f:0.##}B";

            if (value >= 1_000_000)
                return $"{value / 1_000_000f:0.##}M";

            if (value >= 1_000)
                return $"{value / 1_000f:0.##}K";

            return value.ToString();
        }

        public static string GetMemoryString(int megabytes)
        {
            if (megabytes >= 1024)
                return $"{megabytes / 1024f:0.##} GB";

            return $"{megabytes} MB";
        }

        public static string GetFrequencyString(int megahertz)
        {
            if (megahertz <= 0)
            {
                return "N/A";
            }

            if (megahertz >= 1000)
                return $"{megahertz / 1000f:0.##} GHz";

            return $"{megahertz} MHz";
        }
    }
}