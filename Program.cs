using System;
using System.IO;
using System.Linq;

namespace dev275x.studentlist
{
    class Program
    {
        // The Main method 
        static void Main(string[] args)
        {
            //Debbie added check to make sure user added an option
            if (args == null || args.Length!=1) 
            {
               UsageMsg();
               return;
            }

            var fileContents = LoadData("students.txt");
            /* Check arguments */
            //debbie renamed variables for meaning
            if (args[0] == "a") 
            {
                Console.WriteLine("Loading data ...");
                
                var words = fileContents.Split(',');
                foreach(var word in words) 
                {
                    Console.WriteLine(word);
                }
                Console.WriteLine("Data loaded");
            }
            else if (args[0]== "r")
            {
                Console.WriteLine("Loading data ...");
                // We are loading data
                
                var words = fileContents.Split(',');
                var x = new Random();
                var y = x.Next(0,words.Length);
                Console.WriteLine(words[y]);
                Console.WriteLine("Data loaded");
            }

            else if (args[0].Contains("+"))
            {
                // read
                Console.WriteLine("Loading data ...");
                
                var newname = args[0].Substring(1);
                UpdateList(fileContents+","+newname,"students.txt");
                
                Console.WriteLine("Data loaded");
            }
            else if (args[0].Contains("?"))
            {
                Console.WriteLine("Loading data ...");
                var words = fileContents.Split(',');
                bool done = false;
                var t = args[0].Substring(1);
                for (int idx = 0; idx < words.Length && !done; idx++)
                {
                    //Debbie added Trim and corrected brackets
                    if (words[idx].Trim() == t.Trim())  
                    {
                        Console.WriteLine("We found it!");
                        done = true;
                    }
                }
            }
            else if (args[0].Contains("c"))
            {
                Console.WriteLine("Loading data ...");
                var s = new FileStream("students.txt",FileMode.Open);
                var r = new StreamReader(s);
                var D = r.ReadToEnd();
                var a = D.ToCharArray();
                var in_word = false;
                var count = 0;
                foreach(var c in a)
                {
                    if (c > ' ' && c < 0177)
                    {
                        if (!in_word) 
                        {
                            count = count + 1;
                            in_word = true;
                        }
                    }
                    else 
                    {
                        in_word = false;
                    }
                }

                Console.WriteLine(String.Format("{0} words found", count));
           
            }
            else   ///Debbie Added usage message and check for unknown selection
               {
                UsageMsg();
               }
        
        }
   //debbie added LoadData and UpdateList and Updatemsg
        static string LoadData(string fileName)
        {
            string line;
            using (var fileStream = new FileStream(fileName,FileMode.Open))
            using (var reader = new StreamReader(fileStream))
            {
                line=reader.ReadLine();
            }
            return line;
        }  
        static void UpdateList(string content,string fileName)
        {
            var timestamp = String.Format("List last updated on {0}", DateTime.Now);
            using (var fileStream = new FileStream(fileName,FileMode.Open))
            using (var writer = new StreamWriter(fileStream))
            {
                writer.WriteLine(content);
                writer.WriteLine(timestamp);
            }
        }
    static void UsageMsg()
        {
          Console.WriteLine("Specify Options: -a (Show All), -?WORD (Find WORD), -c (Show Count), -+WORD (Adds WORD)");
          return;//exit early
        }
    
        
    }
}