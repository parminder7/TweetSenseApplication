using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSenseApplication.src.twitter;
using TweetSenseApplication.src.fileMgr;
using TweetSenseApplication.src.analyser;

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
            //var otweets = StreamTweet.GetTweetsWithKeyword("worldcup");

            //Encode each tweet and add them to the list
            //var encodedTweets = FileManager.Serializer(otweets);

            //Put the tweets in file
            //var file = FileManager.GetFileName("worldcup");
            //FileManager.Writer(encodedTweets, file);

            //newStreamTweet.tweetStreamer("obama");

            Console.WriteLine(SentimentAnalyser.findSentiment("I love to be a part of team"));
            Console.WriteLine(SentimentAnalyser.findSentiment("I am good but not so good"));
            Console.WriteLine(SentimentAnalyser.findSentiment("LOL!"));
            Console.WriteLine(SentimentAnalyser.findSentiment("on cloud 9!"));
            Console.WriteLine(SentimentAnalyser.findSentiment("I actually didn't like it"));
            Console.WriteLine(SentimentAnalyser.findSentiment("VIDEO: Today @Manuel_Neuer turns 29 who won the adidas Golden Glove at the 2014 #WorldCup\nhttp://t.co/3q80O4VmBe http://t.co/A6u2i89Xuv"));

            Console.ReadKey();
        }
    }
}
