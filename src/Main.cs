using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSenseApplication.src.tweet;

namespace TweetSenseApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Twitter streamer app");
            //Set the Twitter API Credentials
            StreamTweet.SetCredentials();
            Console.WriteLine("Credential OK!");

            //Get the tweets
            var otweets = StreamTweet.GetTweetsWithKeyword("worldcup");

            //Encode each tweet and add them to the list
            var encodedTweets = FileManager.Serializer(otweets);

            //Put the tweets in file
            var file = FileManager.GetFileName("worldcup");
            FileManager.Writer(encodedTweets, file);
        }
    }
}
