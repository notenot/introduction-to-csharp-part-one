using System.Linq;

namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
		    var lastTwoDigits = count % 100;
		    if (new[] {11, 12, 13, 14}.Contains(lastTwoDigits))
		        return "рублей";

		    var lastDigit = count % 10;

		    if (lastDigit == 1)
		        return "рубль";

		    if (new[] {2, 3, 4}.Contains(lastDigit))
		        return "рубля";

		    return "рублей";
		}
	}
}