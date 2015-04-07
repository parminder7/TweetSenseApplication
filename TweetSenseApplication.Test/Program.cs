using System;
using TweetSenseApplication.src.mapreduce;
using Microsoft.Hadoop.MapReduce;

namespace tests
{
    class Program
    {
        
        public static void _Mapper()
        {
            const string inputline = "ID::585304696325328896	Text::The Easter rush has passed and I can function like a normal human being again. #ILoveBeingALineCook #Kinda	Time::2015-04-06 9:54:48 PM	Lattitude::32.745723	Longitude::-80.045119";

            var actualOutput = StreamingUnit.Execute<Mapper>(new[] { inputline });

            //assert
            //var mapperResult = actualOutput.MapperResult;
            Console.WriteLine("Map");
            foreach (var mapperResult in actualOutput.MapperResult)
            {
                Console.WriteLine(mapperResult);
            }
        }

        public static void _Reducer()
        {
            const string neutralInputLine = "ID::585304696325328896	Text::The Easter rush has passed and I can function like a normal human being again. #ILoveBeingALineCook #Kinda	Time::2015-04-06 9:54:48 PM	Lattitude::32.745723	Longitude::-80.045119";
            const string positiveInputLine1 = "ID::585305245741293568	Text::Nasty Easter joke...LMFAO!!!  Brilliant...	Time::2015-04-06 9:56:59 PM	Lattitude::34.10362	Longitude::-118.326781";
            const string positiveInputLine2 = "ID::585305558930104320	Text::I am happy I was in the camp with JPM chief economist on this no cut call and playing GBPAUD short was the Easter bonus.	Time::2015-04-06 9:58:14 PM	Lattitude::51.525835	Longitude::-0.569526";

            var actualOutput = StreamingUnit.Execute<Mapper, Reducer>(
                new[] {neutralInputLine, positiveInputLine1, positiveInputLine2});

            Console.WriteLine("Reduce");
            foreach (var reducerResult in actualOutput.ReducerResult)
            {
                Console.WriteLine(reducerResult);
            }
        }

        static void Main(string[] args)
        {
            _Mapper();
            _Reducer();
            Console.ReadKey();
        }
    }
}
