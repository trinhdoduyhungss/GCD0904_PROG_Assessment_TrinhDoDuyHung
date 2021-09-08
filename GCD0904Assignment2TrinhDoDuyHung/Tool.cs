using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCD0904Assignment2TrinhDoDuyHung
{
    class Tool
    {
        // Gets number in a string
        public static string GetNumber(string userInput)
        {
            /*
                args: string userInput
                des: This function takes a string and returns a string containing the numbers that exist in the input.
                return: string (return == "-1": the input doesn't contain any numbers)
            */
            userInput = new String(userInput.Where(Char.IsDigit).ToArray());
            if (userInput.Length > 0)
            {
                return userInput;
            }
            else
            {
                return "-1";
            }
        }

        // Remove special characters in a string
        public static string RemoveSpecialCharacters(string str)
        {
            /*
                args: string str
                des: This function takes a string and remove special characters.
                     When done, this function returns a new string that doesn't conatin any special characters.
                return: string
            */
            char[] buffer = new char[str.Length];
            int idx = 0;

            foreach (char c in str)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    buffer[idx] = c;
                    idx++;
                }
            }

            return new string(buffer, 0, idx);
        }

        // String matching
        public static double StringMatch(string userInput, string stringSample)
        {
            /*
                args: string userInput, string stringSample
                des: This function takes user input as staff name and calculate matching with sample in "StaffName" array.
                     When done, this function returns the percentage of user input with sample.
                return: double matching
            */
            userInput = RemoveSpecialCharacters(userInput.ToLower());
            stringSample = RemoveSpecialCharacters(stringSample.ToLower());
            char[] input = userInput.ToCharArray();
            char[] sample = stringSample.ToCharArray();
            double matching = 0;
            if (input.Length < sample.Length)
            {
                char[] except = sample.Except(input).ToArray();
                matching = 1 - 1.0 * except.Length / sample.Length;
            }
            else
            {
                char[] except = input.Except(sample).ToArray();
                matching = 1 - 1.0 * except.Length / input.Length;
            }
            return matching;
        }
    }
}
