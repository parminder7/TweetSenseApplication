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
using TweetSenseApplication.src.fileMgr;

namespace TweetSenseApplication.src.twitter
{
    class newStreamTweet
    {
        //member variables
        private static string _accessToken = "";
        private static string _accessTokenSecret = "";
        private static string _consumerKey = "";
        private static string _consumerSecret = "";
        
        /// <summary>
        /// The method streams the real time tweets containing the given keyword
        /// The tweets should be in english language and contain geolocation
        /// </summary>
        /// <param name="keyword"></param>
        public static void tweetStreamer(string keyword)
        {
            _accessToken = ConfigurationManager.AppSettings["token_AccessToken"];
            _accessTokenSecret = ConfigurationManager.AppSettings["token_AccessTokenSecret"];
            _consumerKey = ConfigurationManager.AppSettings["token_ConsumerKey"];
            _consumerSecret = ConfigurationManager.AppSettings["token_ConsumerSecret"];

            var Credentials = TwitterCredentials.CreateCredentials(_accessToken, _accessTokenSecret, _consumerKey, _consumerSecret);
            TwitterCredentials.ExecuteOperationWithCredentials(Credentials, () =>
            {
                var stream = Stream.CreateFilteredStream();
                ICoordinates coordinates1 = new Coordinates(-180, -90);
                ICoordinates coordinates2 = new Coordinates(180, 90);
                ILocation location = new Location(coordinates1, coordinates2);

                stream.AddLocation(location);
                stream.AddTrack("obama");
                stream.AddTweetLanguageFilter(Language.English);

                stream.MatchingTweetAndLocationReceived += (sender, args) =>
                {
                    var tweet = args.Tweet;
                    Console.WriteLine("{0} {1} was detected ", tweet.Id, tweet.Text);

                    //IEnumerable<ILocation> matchingLocations = args.MatchedLocations;

                    processATweet(tweet, keyword);
                    //Console.WriteLine("Text: "+ tweet.Text);
                };

                stream.StartStreamMatchingAllConditions();
            });
        }

        /// <summary>
        /// Helper Method:
        /// This method process on each tweet.
        /// It saves the tweet in a file.
        /// </summary>
        /// <param name="tweet"></param>
        public static void processATweet(ITweet tweet, string filename)
        {
            //foreach (var matchingLocation in matchingLocations)
            //{
            //  Console.Write("({0}, {1}) ;", matchingLocation.Coordinate1.Latitude, matchingLocation.Coordinate1.Longitude);
            //Console.WriteLine("({0}, {1})", matchingLocation.Coordinate2.Latitude, matchingLocation.Coordinate2.Longitude);
            //}
            var downloadedTweets = new List<Tweet>();
            var atweet = new Tweet(tweet);
            downloadedTweets.Add(atweet);
            var encodedTweets = FileManager.Serializer(downloadedTweets);
            var file = FileManager.GetFileName(filename);
            FileManager.Writer(encodedTweets, file);
        }
        
    }
}
