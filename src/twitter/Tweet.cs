using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;

namespace TweetSenseApplication.src.twitter
{
    class Tweet
    {
        public long ID = 0;
        public string Text = "";
        public string User = "";
        public DateTime Time = new DateTime();
        public double longitude, lattitude = 0.0;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Tweet() 
        { 
        }

        /// <summary>
        /// Create a new tweet from a Tweetinvi object
        /// </summary>
        public Tweet(ITweet tobject)
        {
            this.ID = tobject.Id;
            this.Text = tobject.Text.Replace(",", "");
            this.Time = tobject.CreatedAt;
            this.User = tobject.Creator.Name;
            if (tobject.Coordinates != null)
            {
                this.longitude = tobject.Coordinates.Longitude;
                this.lattitude = tobject.Coordinates.Latitude;
            }
        }

        /// <summary>
        /// This method serializes the tweet to a string
        /// </summary>
        public static string Serializer(Tweet line)
        {
            return JsonConvert.SerializeObject(line);
        }

        /// <summary>
        /// This method deserialize the string to Tweet object
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static Tweet Deserializer(string line)
        {
            //Tweet decodedTweet = JsonConvert.DeserializeObject<Tweet>(line);
            //return decodedTweet;

            Dictionary<string, string> decodedTweet = new Dictionary<string,string>();
            decodedTweet = line.Split('\t').Select(
                x => x.Split(new string[] { "::" }, StringSplitOptions.None))
                .ToDictionary(x => x[0], x => x[1]);
            try
            {
                Tweet tweet = new Tweet();
                tweet.ID = Convert.ToInt64(decodedTweet["ID"]);
                tweet.Text = decodedTweet["Text"];
                string timeFormat = "yyyy-MM-dd h:mm:ss tt";
                Console.WriteLine(decodedTweet["Time"]);
                tweet.Time = DateTime.ParseExact(decodedTweet["Time"], timeFormat, CultureInfo.InvariantCulture);
                //tweet.Time = Convert.ToDateTime(decodedTweet["Time"]);
                tweet.lattitude = Convert.ToDouble(decodedTweet["Lattitude"]);
                tweet.longitude = Convert.ToDouble(decodedTweet["Longitude"]);
                return tweet;
            }
            catch { return null; }
            
        }
    }
}
