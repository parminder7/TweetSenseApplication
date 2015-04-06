using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Interfaces.Models;
using Tweetinvi.Core.Interfaces.Streaminvi;
using Tweetinvi.Logic.Model;
using Tweetinvi.Streams;
using Tweetinvi.Core.Events;
using Tweetinvi.Core.Interfaces.oAuth;
using System.Windows;
using Timer = System.Timers.Timer;

namespace TweetSenseApplication.src.twitter
{
    class StreamTweet
    {
        private static string _accessToken = "";
        private static string _accessTokenSecret = "";
        private static string _consumerKey = "";
        private static string _consumerSecret = "";
        private static IFilteredStream filteredStream;

        /// <summary>
        /// Setting up twitter app credentials
        /// </summary>
        public static void SetCredentials()
        {
            //Initializing a Token with Twitter Credentials contained in the App.config 
            _accessToken = ConfigurationManager.AppSettings["token_AccessToken"];
            _accessTokenSecret = ConfigurationManager.AppSettings["token_AccessTokenSecret"];
            _consumerKey = ConfigurationManager.AppSettings["token_ConsumerKey"];
            _consumerSecret = ConfigurationManager.AppSettings["token_ConsumerSecret"];
            TwitterCredentials.SetCredentials(_accessToken, _accessTokenSecret, _consumerKey, _consumerSecret);
        }

        /// <summary>
        /// This method streams the tweets with given keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static List<Tweet> GetTweetsWithKeyword(string keyword)
        {
            var tweets = default(IEnumerable<ITweet>);
            var downloadedTweets = new List<Tweet>();
            
            // Search for all tweets published at a specific location
            //var searchParameter = Search.GenerateTweetSearchParameter(-51.5072, 0.1275, 30, DistanceMeasure.Kilometers);
            filteredStream = Stream.CreateFilteredStream();
            try
            {
                tweets = Search.SearchTweets(keyword); //unable to stream more than 100 tweets
            }
            catch
            {
                Console.WriteLine(" Couldn't download tweets, not trying again...");
                //Thread.Sleep(4000);
            }

            if (tweets != null && tweets.Count() > 0)
            {
                foreach (var tweet in tweets)
                {
                    var atweet = new Tweet(tweet);
                    downloadedTweets.Add(atweet);
                }
            }

            return downloadedTweets;
        }
    }
}
