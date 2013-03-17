using System;

static class RandomLetter
{
    static Random random = new Random();

    /// <summary>
    /// This method returns a random lowercase letter.
    ///  Between 'a' and 'z' inclusize.
    /// </summary>
    /// <returns>Lowercase Letter</returns>
    public static char GetLowercasetLetter()
    {
        int num = random.Next(0, 26); // Zero to 25
        char let = (char)('a' + num);
        return let;
    }

    /// <summary>
    /// This method returns a random uppercase letter.
    ///  Between 'a' and 'z' inclusize.
    /// </summary>
    /// <returns>Lowercase Letter</returns>
    public static char GetUppercasetLetter()
    {
        // This method returns a random lowercase letter.
        // ... Between 'a' and 'z' inclusize.
        int num = random.Next(0, 26); // Zero to 25
        char let = (char)('A' + num);
        return let;
    }
}
