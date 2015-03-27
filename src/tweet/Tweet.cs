using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;

namespace TweetSenseApplication.src.tweet
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
        public Tweet(ITweet obj)
        {
            this.ID = obj.Id;
            this.Text = obj.Text.Replace(",", "");
            this.Time = obj.CreatedAt;
            this.User = obj.Creator.Name;
            //if (original.Coordinates != null)
            //{
            //    this.longitude = original.Coordinates.Longitude;
            //    this.lattitude = original.Coordinates.Latitude;
            //}
        }

        /// <summary>
        /// This method serializes the tweet to a string
        /// </summary>
        public static string Serializer(Tweet line)
        {
            return JsonConvert.SerializeObject(line);
        }
    }
}
