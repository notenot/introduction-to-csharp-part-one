/*
Вася сменил пароль на новый и забыл его!

На этот раз он точно помнит, что он сконструировал пароль из старого пароля, поменяв регистр нескольких букв. Он, конечно, не хочет вам говорить старый пароль, поэтому просит написать программу, которая по заданному слову перебирает все возможные пароли, полученные из этого слова заменой регистра.

Для удобства Вася просит, чтобы пароли появлялись в лексикографическом порядке, считая, что маленькие буквы меньше больших. Естественно, регистр нужно менять только, у букв.

Например, для входного слова 'ab42' результат должен быть такой: 'ab42', 'aB42', 'Ab42', 'AB42'

На вход подается слово в нижнем регистре. В результирующем списке не должно быть повторений слов.
*/

public class CaseAlternatorTask
{
    public static List<string> AlternateCharCases(string lowercaseWord)
    {
        var result = new List<string>();
        AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
        return result;
    }

    static void AlternateCharCases(char[] word, int startIndex, List<string> result)
    {
    		var currentWord = new string(word);
    		if (!result.Contains(currentWord))
    			result.Add(currentWord);

    		if (startIndex == word.Length)
    			return;

    		AlternateCharCases(word.ToArray(), startIndex + 1, result);

    		if (char.IsLetter(word[startIndex]))
    		{
    			word[startIndex] = char.ToUpper(word[startIndex]);
    			AlternateCharCases(word.ToArray(), startIndex + 1, result);
    		}
    }
}
