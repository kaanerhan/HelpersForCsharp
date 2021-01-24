using System;
using System.IO;
using System.Text;

namespace Helpers.Modules
{
    class Log
    {
        public enum Log_Type
        {
            Error,
            Information,
            Warning,
            Success
        }
        public static void Write(string module, Log_Type log_type, string log_message)
        {
            //File operations sometimes go wrong.As a result i used loop for try again
            //You can set parameter Log_Directory in Write method or open a variable in Settings (Properties.Settings.Default.Log_Directory)
            //I used StringBuilder for use Ram efficiently
            string directory = "";
            string file_name = "";
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (Properties.Settings.Default.Log_Directory.Equals(""))
                    {
                        stringBuilder.Append(Directory.GetCurrentDirectory());
                        stringBuilder.Append("\\Log");
                        directory = stringBuilder.ToString();
                        stringBuilder = new StringBuilder();
                    }
                    else
                        directory = Properties.Settings.Default.Log_Directory;
                    
                    stringBuilder.Append(System.Reflection.Assembly.GetEntryAssembly().GetName().Name);
                    stringBuilder.Append(DateTime.Now.ToString("yyyyMMdd"));
                    stringBuilder.Append(".log");
                    file_name = stringBuilder.ToString();
                    stringBuilder = new StringBuilder();

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
                    StreamWriter file;

                    stringBuilder.Append(directory);
                    stringBuilder.Append("\\");
                    stringBuilder.Append(file_name);
                    file = File.AppendText(stringBuilder.ToString());
                    stringBuilder = new StringBuilder();

                    stringBuilder.Append(DateTime.Now.ToString());
                    stringBuilder.Append("      ");
                    stringBuilder.Append(module);
                    stringBuilder.Append("      ");
                    stringBuilder.Append(log_type.ToString());
                    stringBuilder.Append("      ");
                    stringBuilder.Append(log_message);
                    file.WriteLine(stringBuilder.ToString());
                    file.Close();
                    break;
                }
                catch
                {
                }
            }
        }
    }
}
