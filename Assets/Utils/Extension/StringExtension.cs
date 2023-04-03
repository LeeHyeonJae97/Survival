using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtension
{
    public static string ReplaceExcept(this string str, char[] exceptChars, char newChar)
    {
        char[] charArray = str.ToCharArray();

        for (int i = 0; i < charArray.Length; i++)
        {
            bool except = false;

            for (int j = 0; j < exceptChars.Length; j++)
            {
                if (charArray[i] == exceptChars[j])
                {
                    except = true;
                    break;
                }
            }

            if (!except) charArray[i] = newChar;
        }

        return new string(charArray);
    }
}
