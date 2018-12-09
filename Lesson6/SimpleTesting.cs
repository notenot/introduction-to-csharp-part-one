/*
На вход программе подается строка текста.
На выход нужно вернуть массив полей, извлечённых из входа, либо пустой массив,
если полей нет.

Поля могут быть двух типов:
1. Простые поля
    Не могут быть пустыми, не могут содержать пробелов и разделяются одним или
    несколькими пробелами.

2. Поля в кавычках
    * Могут содержать пробелы и быть пустыми.
    * Кавычки разных типов могут быть вложенными.
    * Поля, заключенные в кавычки, могут не отделяться от других полей пробелами.
    * Если в строке отсутствует последняя парная закрывающая кавычка, считать,
      что соответствующее поле заканчивается в конце строки.
    * Поле внутри кавычек может содержать символы кавычек, экранированные
      символом '\'. Символ '\' также может быть экранирован самим же собой.
    * В простых полях символ '\' не считается экранирующим символом.

Игнорируйте пробелы в начале или в конце строки, если они не входят в поле.

В этой задаче вам не нужно реализовывать алгоритм. Вместо этого напишите набор
тестов, который покрывает все основные ситуации для данной задачи.

Используйте метод Test(string input, string[] expectedOutput), который принимает
первым аргументом входные данные, а вторым ожидаемый вывод.
*/

public static void RunTests()
{
  EmptyStringAndWhitespaces();
	LeadingAndtTrailingSpaces();
	EscapeCharacters();
	FieldTypesCombinations();
	UnclosedQuotationMarksAndApostrophes();
	NestedQuotationMarksAndApostrophes();
	SpacesInQuotationMarksAndApostrophes();
}

public static void EmptyStringAndWhitespaces()
{
	Test("", new string[] {});
	Test(" ", new string[] {});
	Test("  ", new string[] {});
	Test("   ", new string[] {});
	Test("    ", new string[] {});
}

public static void LeadingAndtTrailingSpaces()
{
	Test(" a", new string[] { "a" });
	Test("   a", new string[] { "a" });

	Test("a ", new string[] { "a" });
	Test("a   ", new string[] { "a" });

	Test(" a  ", new string[] { "a" });
	Test("               a ", new string[] { "a" });
	Test("     a           ", new string[] { "a" });
}

public static void EscapeCharacters()
{
	Test("\\\\", new string[] { "\\\\" });

	Test("'\\\"\\\"'", new string[] { "\"\"" });
	Test("\"\\\"\\\"\"", new string[] { "\"\"" });

	Test("'\\\'\\\''", new string[] { "\'\'" });
	Test("\"\\\'\\\'\"", new string[] { "\'\'" });

	Test("\"\\\\\"", new string[] { "\\" });
}

public static void FieldTypesCombinations()
{
	Test("a'b'c", new string[] { "a", "b", "c" });
	Test("'a'b'c'", new string[] { "a", "b", "c" });

	Test("a\"b\"c", new string[] { "a", "b", "c" });
	Test("\"a\"b\"c\"", new string[] { "a", "b", "c" });
}

public static void UnclosedQuotationMarksAndApostrophes()
{
	Test("\"", new string[] { "" });
	Test("\'", new string[] { "" });
	Test("\" ", new string[] { " " });
	Test("' ", new string[] { " " });
}

public static void NestedQuotationMarksAndApostrophes()
{
	Test("\"''\"", new string[] { "''" });
	Test("\"''''\"", new string[] { "''''" });

	Test("'\"\"'", new string[] { "\"\"" });
	Test("'\"\"\"\"'", new string[] { "\"\"\"\"" });
}

public static void SpacesInQuotationMarksAndApostrophes()
{
	Test("' '", new string[] { " " });
	Test("' a '", new string[] { " a " });
	Test("'b b'", new string[] { "b b" });

	Test("\" \"", new string[] { " " });
	Test("\" a \"", new string[] { " a " });
	Test("\"b b\"", new string[] { "b b" });
}
