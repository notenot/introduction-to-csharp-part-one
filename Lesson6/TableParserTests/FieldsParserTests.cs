using TableParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TableParserTests
{
    [TestClass]
    public class FieldsParserTests
    {
        public void TestParseLine(string input, string[] expectedOutput)
        {
            var result = FieldsParserTask.ParseLine(input).ToArray();

            Assert.AreEqual(expectedOutput.Length, result.Length);

            for (var i = 0; i < result.Length; ++i)
                Assert.AreEqual(expectedOutput[i], result[i]);
        }

        [TestMethod]
        public void EmptyStringAndWhitespaces()
        {
            TestParseLine("", new string[] { });
            TestParseLine(" ", new string[] { });
            TestParseLine("  ", new string[] { });
            TestParseLine("   ", new string[] { });
            TestParseLine("    ", new string[] { });
        }

        [TestMethod]
        public void LeadingAndtTrailingSpaces()
        {
            TestParseLine(" a", new[] { "a" });
            TestParseLine("   a", new[] { "a" });

            TestParseLine("a ", new[] { "a" });
            TestParseLine("a   ", new[] { "a" });

            TestParseLine(" a  ", new[] { "a" });
            TestParseLine("               a ", new[] { "a" });
            TestParseLine("     a           ", new[] { "a" });
        }

        [TestMethod]
        public void EscapeCharacters()
        {
            TestParseLine("\\\\", new[] { "\\\\" });

            TestParseLine("'\\\"\\\"'", new[] { "\"\"" });
            TestParseLine("\"\\\"\\\"\"", new[] { "\"\"" });

            TestParseLine("'\\\'\\\''", new[] { "\'\'" });
            TestParseLine("\"\\\'\\\'\"", new[] { "\'\'" });

            TestParseLine("\"\\\\\"", new[] { "\\" });
        }

        [TestMethod]
        public void FieldTypesCombinations()
        {
            TestParseLine("a'b'c", new[] { "a", "b", "c" });
            TestParseLine("'a'b'c'", new[] { "a", "b", "c" });

            TestParseLine("a\"b\"c", new[] { "a", "b", "c" });
            TestParseLine("\"a\"b\"c\"", new[] { "a", "b", "c" });
        }

        [TestMethod]
        public void UnclosedQuotationMarksAndApostrophes()
        {
            TestParseLine("\"", new[] { "" });
            TestParseLine("\'", new[] { "" });
            TestParseLine("\" ", new[] { " " });
            TestParseLine("' ", new[] { " " });
        }

        [TestMethod]
        public void NestedQuotationMarksAndApostrophes()
        {
            TestParseLine("\"''\"", new[] { "''" });
            TestParseLine("\"''''\"", new[] { "''''" });

            TestParseLine("'\"\"'", new[] { "\"\"" });
            TestParseLine("'\"\"\"\"'", new[] { "\"\"\"\"" });
        }

        [TestMethod]
        public void SpacesInQuotationMarksAndApostrophes()
        {
            TestParseLine("' '", new[] { " " });
            TestParseLine("' a '", new[] { " a " });
            TestParseLine("'b b'", new[] { "b b" });

            TestParseLine("\" \"", new[] { " " });
            TestParseLine("\" a \"", new[] { " a " });
            TestParseLine("\"b b\"", new[] { "b b" });
        }
    }
}
