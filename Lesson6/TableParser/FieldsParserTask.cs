using System.Collections.Generic;
using System.Text;

namespace TableParser
{
	public class FieldsParserTask
	{
		public static List<string> ParseLine(string line)
		{
            var result = new List<string>();
		    var startIndex = SkipLeadingSpaces(line, 0);
		    while (startIndex < line.Length && line.Length > 0) 
		    {
                var token = ReadField(line, startIndex);
                result.Add(token.Value);
		        startIndex = SkipLeadingSpaces(line, token.GetIndexNextToToken());
		    }
		    return result;
		}
        
		private static Token ReadField(string line, int startIndex)
		{
		    if (line[startIndex] == '\'' || line[startIndex] == '\"')
		    {
		        var endIndex = GetIndexOfNextClosingQuotationMark(line, startIndex);
                return endIndex != -1 ? 
                    CreateToken(line, startIndex, endIndex - startIndex + 1) : 
                    CreateToken(line, startIndex, line.Length - startIndex); 
		    }
            
            return ReadSimpleField(line, startIndex);
		}

	    private static Token CreateToken(
	        string line, int startIndex, int length, bool isSimpleField = false)
	    {
	        var valueBuilder = new StringBuilder();
	        for (var i = 0; i < length; ++i)
	        {
	            var currentChar = line[i + startIndex];
	            var currentValue = currentChar;
                if (currentChar == '\\' && !isSimpleField)
                    if (i + startIndex < line.Length - 1)
                    {
                        currentValue = line[i + startIndex + 1];
                        ++i;
                    }
                    else continue;
                if ((currentChar == '\"' || currentChar == '\'' && !isSimpleField) 
                    && (i == 0 || i == length - 1))
                    continue;
                valueBuilder.Append(currentValue);
            }
	        return new Token(valueBuilder.ToString(), startIndex, length);
        }

	    private static int SkipLeadingSpaces(string line, int startIndex)
	    {
	        var newStartIndex = startIndex;

	        while (newStartIndex < line.Length && line[newStartIndex] == ' ')
	            ++newStartIndex;

            return newStartIndex;
	    }
        
        private static int GetIndexOfNextClosingQuotationMark(string line, int startIndex)
        {
            var quotationMark = line[startIndex];

            var lineWithoutEscapeQuotationMarks = 
                line.Replace($"\\\\", "..")
                    .Replace($"\\{quotationMark}", "..");
            
            return lineWithoutEscapeQuotationMarks
                .IndexOf(quotationMark, startIndex + 1);
	    }
        
	    private static Token ReadSimpleField(string line, int startIndex)
	    {
	        var endIndex = line.IndexOfAny(new[] { '\"', '\'', ' ' }, startIndex);
	        if (endIndex == -1) endIndex = line.Length;
	        return CreateToken(line, startIndex, endIndex - startIndex, true);
	    }
    }
}