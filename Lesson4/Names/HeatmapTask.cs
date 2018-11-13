namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var daysCount = 30;
            var days = new string[daysCount];
            for (var i = 0; i < daysCount; ++i)
                days[i] = (i + 2).ToString();

            var monthsCount = 12;
            var months = new string[monthsCount];
            for (var i = 0; i < monthsCount; ++i)
                months[i] = (i + 1).ToString();

            var heatmap = new double[daysCount, monthsCount];
            foreach (var name in names)
            {
                if (name.BirthDate.Day == 1)
                    continue;

                ++heatmap[name.BirthDate.Day - 2, name.BirthDate.Month - 1];
            }

            return new HeatmapData("Карта интенсивностей", heatmap, days, months);
        }
    }
}