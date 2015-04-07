using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetSenseApplication.src.mapreduce
{
    class MRRunner
    {
        public static void run()
        {
            //HadoopJobConfiguration config = new HadoopJobConfiguration();
            //config.InputPath = "/userme/HDIUser/input/worldcup.txt";
            //config.OutputFolder = "/userme/HDIUser/output";
            //Uri hduri = new Uri("https://TAnalyser.azurehdinsight.net");
            //string hadoopUserName = "admin1";

            //// Azure Storage Information.
            //string azureStorageAccount = "tweetbase.blob.core.windows.net";
            //string azureStorageKey = "QfrZs8Jwyeu1DDoxZ+0aVoUUsmH02ssTAH0BN8JPUq8sZZF5KowBREU9icS506DEFiuZIafLMg2RsUo3tWq2XA==";
            //string azureStorageContainer = "tanalyser";
            //bool createContinerIfNotExist = true;

            //Environment.SetEnvironmentVariable("HADOOP_HOME", @"c:\hadoop"); //Workaround for environment variable issue
            //IHadoop hadoop = Hadoop.Connect(hduri, "admin", hadoopUserName, "ABC!123abc", azureStorageAccount, azureStorageKey, azureStorageContainer, createContinerIfNotExist);
            //hadoop.MapReduceJob.Execute<Mapper, Reducer>(config);
            //Console.Read();

            var hadoop = connectAzure();
            var result = hadoop.MapReduceJob.ExecuteJob<Job>();
        }

        public class Job : HadoopJob<Mapper, Reducer>
        {
            public override HadoopJobConfiguration Configure(ExecutorContext context)
            {
                HadoopJobConfiguration config = new HadoopJobConfiguration();
                config.InputPath = "asv://tanalyser@tweetbase.blob.core.windows.net/tweet/input/furious7.txt";
                config.OutputFolder = "asv://tanalyser@tweetbase.blob.core.windows.net/tweet/output";
                return config;
            }
        }

        public static IHadoop connectAzure()
        {
            Environment.SetEnvironmentVariable("HADOOP_HOME", @"c:\hadoop"); //Workaround for environment variable issue
            return Hadoop.Connect(
                new Uri("https://TAnalyser.azurehdinsight.net"),
                "admin", "admin1", "ABC!123abc",
                "tweetbase.blob.core.windows.net",
                "QfrZs8Jwyeu1DDoxZ+0aVoUUsmH02ssTAH0BN8JPUq8sZZF5KowBREU9icS506DEFiuZIafLMg2RsUo3tWq2XA==",
                "tanalyser",
                true
                );
        }
    }
}
