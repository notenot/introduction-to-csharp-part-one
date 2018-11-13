namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var daysСount = 31;
            var days = new string[daysСount];
            for (var d = 0; d < daysСount; d++)
                days[d] = (d + 1).ToString();

            var birthsCounts = new double[daysСount];
            foreach (var nameData in names)
            {
                if (nameData.Name != name || nameData.BirthDate.Day == 1)
                    continue;

                birthsCounts[nameData.BirthDate.Day - 1]++;
            }

            return new HistogramData(string.Format("Рождаемость людей с именем '{0}'", name), days, birthsCounts);
        }
    }
}