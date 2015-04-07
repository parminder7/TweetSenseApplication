using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.sentiment;
using java.util;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.util;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.neural.rnn;
using edu.stanford.nlp.parser.lexparser;


namespace TweetSenseApplication.src.analyser
{
    class SentimentAnalyser
    {

        public static string findSentiment (String line)
        {
        //    // Path to models extracted from `stanford-parser-3.5.1-models.jar`
        //    var jarRoot = @"..\..\..\..\paket-files\nlp.stanford.edu\stanford-parser-full-2015-01-30\models\";
        //    var modelsDirectory = jarRoot + @"\edu\stanford\nlp\models";
            //var modelsDirectory = @"\edu\stanford\nlp\models";

            var jarRoot = @"E:\SONCHIRDI_STUDY_MATERIAL\MSS_TEXTBOOKS\Term4-Text books\TWITTER_SentimentAnalysis\TweetSenseApplication\TweetSenseApplication\src\stanford-corenlp\";
            //var jarRoot = @"..\..\src\stanford-corenlp\";
            
            //var modelsDirectory = jarRoot + @"\edu\stanford\nlp\models";            
            // Loading english PCFG parser from file
            //var lp = LexicalizedParser.loadModel(modelsDirectory + @"\lexparser\englishPCFG.ser.gz");

            Properties prop = new Properties();
            prop.setProperty("annotators", "tokenize, ssplit, parse, sentiment");

            var curDir = Environment.CurrentDirectory;
            System.IO.Directory.SetCurrentDirectory(jarRoot);
            
            StanfordCoreNLP pipeline = new StanfordCoreNLP(prop);
            System.IO.Directory.SetCurrentDirectory(curDir);

            String[] polarity = { "Very Negative", "Negative", "Neutral", "Positive", "Very Positive" };
            int score = 0; 

            if ((line != null) && (line.Length > 0))
            {
                Annotation annotation = new Annotation(line);
                pipeline.annotate(annotation);

                foreach (CoreMap sent in (dynamic)annotation.get(new CoreAnnotations.SentencesAnnotation().getClass()))
                {
                    Tree tree = (Tree)sent.get(new SentimentCoreAnnotations.AnnotatedTree().getClass());
                    score = RNNCoreAnnotations.getPredictedClass(tree);
                    //Console.WriteLine("The polarity of the satement is "+polarity[score]);
                }
            }
            return polarity[score];
        }
    }
}
