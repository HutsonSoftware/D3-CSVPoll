using System;
using System.Windows.Forms;

namespace HutSoft.D3.CSVPoll
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    CSVPollBO csvPollBO = new CSVPollBO();
                    CommandLineParser parser = new CommandLineParser(args);
                    string csvFile = parser["csvFile"];
                    string serverInstance = parser["serverInstance"];
                    string database = parser["database"];
                    if (csvFile != null && serverInstance != null && database != null)
                    {
                        csvPollBO.ImportCsvValuesToDB(csvFile, serverInstance, database);
                        Console.WriteLine("Import Complete");
                    }
                    else
                    {
                        Console.WriteLine("The parameters \"csvFile\", \"serverInstance\", and \"database\" are required.");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CSVPollUI());
            }
        }
    }
}
