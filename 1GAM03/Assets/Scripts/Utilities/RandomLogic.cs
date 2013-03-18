using UnityEngine;
using System.Linq;
using System.Text;

static class RandomLogic
{
    const string lettersRange = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    /// <summary>
    /// Get random characters shuffled
    /// </summary>
    /// <returns>Random character shuffled</returns>
    public static string GetLettersShuffled()
    {
        StringBuilder rs = new StringBuilder();
        rs.Append(lettersRange[Random.Range(0, lettersRange.Length)]);

        return rs.ToString();
    }

    /// <summary>
    /// Get a random letter Between 'a' and 'z' inclusize.
    /// </summary>
    /// <returns>Lowercase Letter</returns>
    public static char GetLowercasetLetter()
    {
        int num = Random.Range(0, 26); // Zero to 25
        char let = (char)('a' + num);
        return let;
    }

    /// <summary>
    /// Get a random letter Between 'A' and 'Z' inclusize.
    /// </summary>
    /// <returns>Uppercase Letter</returns>
    public static char GetUppercasetLetter()
    {
        int num = Random.Range(0, 26); // Zero to 25
        char let = (char)('A' + num);
        return let;
    }

    /// <summary>
    /// Get a random color between those who
    /// have a base common color name
    /// </summary>
    /// <returns>Lowercase Letter</returns>
    public static Color GetColor()
    {
        return new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
    }
}
