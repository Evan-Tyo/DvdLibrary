using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Npgsql;

namespace DvdLibrary.Scripts
{
    public class RunScript
    {
        // Set up the connection string from Web.config
        private static string _connectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;

        static int previousHour = DateTime.Now.Hour;
        static int count = 0;

        public static void ReadScript()
        {
            NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
            FileInfo file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/Scripts", "Script.txt"));
            string script = file.OpenText().ReadToEnd();
            NpgsqlCommand command = new NpgsqlCommand(script, conn);

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void EveryHourScript()
        {
            if (count == 0)
            {
                ReadScript();
                count++;
            }

            var timer = new System.Timers.Timer(60 * 1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            timer.Start();
        }

        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            
            if (previousHour < DateTime.Now.Hour || (previousHour == 23 && DateTime.Now.Hour == 0))
            {
                previousHour = DateTime.Now.Hour;
                ReadScript();
            }
        }
    }
}