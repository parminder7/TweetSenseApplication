using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSenseApplication.src.analyser;
using TweetSenseApplication.src.reverseGeoCoding;
using TweetSenseApplication.src.twitter;

namespace TweetSenseApplication.src.mapreduce
{
    public class Mapper : MapperBase
    {
        public override void Map(String line, MapperContext context)
        {
            Console.WriteLine(line);
            Tweet atweet = Tweet.Deserializer(line);
            string polarity = SentimentAnalyser.findSentiment(atweet.Text);

            int year = atweet.Time.Year;
            int month = atweet.Time.Month;

            string country = Country.getCountryByLongitudeLattitude(atweet.lattitude, atweet.longitude);

            if (year == 2015 || year == 2014)
            {
                context.IncrementCounter("HitMissProgress", "TweetOfRecentYearFound", 1);
                context.EmitKeyValue(country, polarity);
            }
            context.IncrementCounter("HitMissProgress", "TweetOfRecentYearNOTFound", 1);
        }
    }
}
