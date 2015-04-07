using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetSenseApplication.src.mapreduce
{
    class MRStringUtil
    {
        /// <summary>
        /// This method converts the array of string to string
        /// where each element is separated by tab
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string convertToTabbedString(string[] input)
        {
            StringBuilder output = new StringBuilder();
            foreach (var i in input)
            {
                if (output.Length > 0)
                    output.Append("\t");
                output.Append(i);
            }
            return output.ToString();
        }

        /// <summary>
        /// This method converts a string to string array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[] getAsStringVector(string input)
        {
            return input.Split('\t');
        }
    }
}
