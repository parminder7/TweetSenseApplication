using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TweetSenseApplication.src.tweet
{
    class FileManager
    {
        /// <summary>
        /// This method returns the file name as per the filter keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static string GetFileName(string keyword)
        {
            return @"../../data/" + keyword + ".txt";
        }

        /// <summary>
        /// This method converts the list of Tweet object to list of string
        /// </summary>
        /// <param name="tweetList"></param>
        /// <returns></returns>
        public static List<string> Serializer(List<Tweet> tweetList)
        {
            var encodedList = new List<string>();
            foreach (var line in tweetList)
            {
                var encodedTweet = Tweet.Serializer(line);
                encodedList.Add(encodedTweet);
            }
            return encodedList;
        }
        
        /// <summary>
        /// This method writes the tweet list to the file
        /// </summary>
        /// <param name="TweetList"></param>
        /// <param name="file"></param>
        public static void Writer(List<string> TweetList, string file)
        {
            var writer = new StreamWriter(file, true);
            if (TweetList != null && TweetList.Count() > 0)
            {
                foreach (var tweet in TweetList)
                {
                    writer.WriteLine(tweet);
                }
            }
            writer.Close();
        }

    }
}
