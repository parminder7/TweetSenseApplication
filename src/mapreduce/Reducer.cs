using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetSenseApplication.src.mapreduce
{
    public class Reducer : ReducerCombinerBase
    {
        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            var valuesForCountryKey = values as List<string> ?? values.ToList();
            var totalEvidences = valuesForCountryKey.Count();
            var totalPositiveResponses = valuesForCountryKey.Count(val => val == "Positive" || val == "Very Positive");
            var totalNeutralResponses = valuesForCountryKey.Count(val => val == "Neutral");
            var totalNegativeResponses = valuesForCountryKey.Count(val => val == "Negative" || val == "Very Negative");

            context.EmitKeyValue(key, MRStringUtil.convertToTabbedString(
                                    new[]{
                                        (totalPositiveResponses/totalEvidences).ToString("P", CultureInfo.InvariantCulture),
                                        (totalNeutralResponses/totalEvidences).ToString("P", CultureInfo.InvariantCulture),
                                        (totalNegativeResponses/totalEvidences).ToString("P", CultureInfo.InvariantCulture),
                                    }));
        }
    }
}
