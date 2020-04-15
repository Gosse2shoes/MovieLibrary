using System;
using System.IO;
using NLog;


namespace MovieLibraryApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // create NLog configuration
            var config = new NLog.Config.LoggingConfiguration();

            // define targets
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log_file.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // specify minimum log level to maximum log level and target (console, file, etc.)
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

            // apply NLog configuration
            NLog.LogManager.Configuration = config;

            // create instance of LogManager
            var logger = NLog.LogManager.GetCurrentClassLogger();
            var file = "movies.csv";
            
            if (!File.Exists(file))
            {
                Console.WriteLine("File does not exist");
                logger.Warn("File does not exist");
            }
            else
            {
                
                string choice = "";
                    do
                    {
                    Console.WriteLine("1. Display Movies \n2. Add a movie \n3. Quit");
                    choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        logger.Info("Starting to read file");
                            StreamReader sr = new StreamReader(file);
                            while (!sr.EndOfStream)
                        {
                            string movielist = sr.ReadLine();
                            string[] movie = movielist.Split(',');
                            string movies = movie[1] + (movie.Length > 3 ? ", " + movie[2] : "");
                            Console.WriteLine(movies);
                                
                            }
                        sr.Close();
                        logger.Info("Finished reading file");
                    }
                       
                    else if(choice == "2")
                        {
                        logger.Info("Starting to read file");
                        StreamReader sr = new StreamReader(file);
                            int ID = 1;
                        Boolean right = true;
                            Console.WriteLine("Title of the movie: ");
                            string title = Console.ReadLine();
                        if(title.Contains("The") && title.IndexOf("The ") == 0)
                        {
                            title = title.Substring(4) + ", The";
                        }
                            Console.WriteLine("Year of release: ");
                            string year = Console.ReadLine();
                        title += " (" + year + ")";
                            Console.WriteLine("The movie's Genre: ");
                            string genre = Console.ReadLine();
                        string response = "";
                        string other = "";
                        Console.WriteLine("Is there another genre in this moive (Y/N): ");
                        response = Console.ReadLine();
                        while (response == "y" || response == "Y")
                        {
                            Console.WriteLine("What is the other genre: ");
                            other += "|" + Console.ReadLine();
                            Console.WriteLine("Is there another genre in this moive (Y/N): ");
                            response = Console.ReadLine();
                        }
                        genre += other;
                            while (!sr.EndOfStream)
                            {
                                
                                string movielist = sr.ReadLine();
                                string[] movie = movielist.Split(',');
                                string movietitle = movie[1] + (movie.Length > 3 ? ", " + movie[2] : "");
                                
                                if (title.Equals(movietitle))
                                {
                                    Console.WriteLine("This title already is on this list");
                                    logger.Warn("Duplicate");
                                    right = false;
                                    break;
                                }
                            if (sr.EndOfStream)
                            {
                                ID = int.Parse(movie[0]) + 1;
                            }
                            
                            }
                        sr.Close();
                        logger.Info("Finished reading file");

                        if (right)
                        {
                            logger.Info("Start writing to file");
                            StreamWriter sw = new StreamWriter(file, append: true);
                            sw.WriteLine("{0},\"{1}\", {2}", ID, title, genre);
                            logger.Info("Finished writing to file");
                            sw.Close();
                        }
                    }

                } while (choice == "1" || choice == "2");
            }
                
            }
        }
    }
