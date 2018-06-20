using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Diagnostics;

namespace CodeLibrary_Patcher
{
    class Program
    {
        static WebClient client = new WebClient();
        static void Main(string[] args)
        {
            client.Encoding = Encoding.UTF8;
            Compare();
            Console.ReadLine();
        }

        private static void Compare()
        {
            float version = float.Parse(client.DownloadString("https://raw.githubusercontent.com/devangelinos/CodeLibrary/master/Version"), CultureInfo.InvariantCulture); 
            float localVersion = float.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\version.txt"), CultureInfo.InvariantCulture);

            //comapre the versions

            if(version > localVersion)
            {
                Console.WriteLine("Downloading new version...");
                try
                {
                    client.DownloadFile("https://github.com/devangelinos/CodeLibrary/releases/download/1.0/Release.zip", AppDomain.CurrentDomain.BaseDirectory + "\\NewVersion.zip");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Applying update...");
                
                

                //ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\NewVersion.zip", AppDomain.CurrentDomain.BaseDirectory);
                ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\NewVersion.zip", AppDomain.CurrentDomain.BaseDirectory+"\\New Version");

                string[] newFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "New Version");

                foreach(string file in newFiles)
                {
                    string fileName = file.Substring(file.LastIndexOf("\\")+1);
                    if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + fileName) && fileName != "CodeLibrary_Patcher.exe")
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory +  fileName);
                        Console.WriteLine(fileName+" deleted");
                        File.Move(file, AppDomain.CurrentDomain.BaseDirectory +  fileName);
                        Console.WriteLine(fileName+" moved");
                    }
                }

                //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\CodeLibrary.exe"))
                //{
                //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\CodeLibrary.exe");
                //    Console.WriteLine("CodeLibrary.exe deleted");
                //    File.Move(AppDomain.CurrentDomain.BaseDirectory + "\\New Version\\CodeLibrary.exe", AppDomain.CurrentDomain.BaseDirectory + "\\CodeLibrary.exe");
                //    Console.WriteLine("CodeLibrary.exe moved");
                //}
                //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Newtonsoft.Json.dll"))
                //{
                //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\Newtonsoft.Json.dll");
                //    Console.WriteLine("Newtonsoft deleted");
                //    File.Move(AppDomain.CurrentDomain.BaseDirectory + "\\New Version\\Newtonsoft.Json.dll", AppDomain.CurrentDomain.BaseDirectory + "\\Newtonsoft.Json.dll");
                //    Console.WriteLine("Newtonsoft moved");
                //}
                
                
                //Delete the zip file
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\NewVersion.zip");

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\version.txt", version.ToString());
                if(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\New Version").Length == 0)
                {
                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\New Version");
                }

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Application is now up to date");
            }
            else 
            {
                Console.WriteLine("Application is up to date");
            }
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\CodeLibrary.exe");
        }

    }
}
