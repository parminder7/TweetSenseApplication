using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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


    }
}
