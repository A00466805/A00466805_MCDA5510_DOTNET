using System;
using System.Globalization;
using System.IO;

using System.Collections;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace Assignment1
{


    public class CustomerInfo
    {
        [Name("First Name")]
        public String fName { get; set; }
        [Name("Last Name")]
        public String lName { get; set; }
        [Name("Street Number")]
        public String streetnum { get; set; }
        [Name("Street")]
        public String street { get; set; }
        [Name("City")]
        public String city { get; set; }
        [Name("Province")]
        public String province { get; set; }
        [Name("Postal Code")]
        public String postalcode { get; set; }
        [Name("Country")]
        public String country { get; set; }
        [Name("Phone Number")]
        public String phonenum { get; set; }
        [Name("email Address")]
        public String email { get; set; }
        [Name("Date")]
        [Optional]
        public String date { get; set; }

    }

    public class DirectoryWalker 
            {
                private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                private static int count = 0;

                private static int countRows = 0;

                private static List<CustomerInfo> globalList = new List<CustomerInfo>();

                public void walk(String path)
                {
                    if (!String.IsNullOrEmpty(path))
                    {
                        try
                        {
                           string[] list = Directory.GetDirectories(path);
                



                        if (list == null) return;

                        foreach (string dirpath in list)
                        {
                            if (Directory.Exists(dirpath))
                            {
                                walk(dirpath);
                                Console.WriteLine("Dir:" + dirpath + "Length: " + dirpath.Length);
                                //log.Info("Dir: " + dirpath);
                            }


                        }
                                string[] fileList = Directory.GetFiles(path);
                                foreach (string filepath in fileList)
                                {

                                    Console.WriteLine("File:" + filepath);
                                    if (filepath.Contains(".csv"))
                                    {
                                        readFile(filepath, filepath.Substring(68, 10));
                                    }
                                    //log.Info("File: " + filepath);
                                    //81834
                                }
                        
                        }
                        catch (DirectoryNotFoundException)
                        {
                            log.Error("The directory cannot be found.");
                        }
                    }
                }

            public static void readFile(string currentFile,String date)
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    NewLine = Environment.NewLine,
                    MissingFieldFound = null
                };
                try
                {
                    using (var sr = new StreamReader(currentFile))
                    {
                        var reader = new CsvReader(sr, config);
                        var dataRecords = reader.GetRecords<CustomerInfo>();
                        var recordList = dataRecords.ToList();
                        List<CustomerInfo> removeFromList = new List<CustomerInfo>();
                        foreach (CustomerInfo record in recordList)
                        {
                            //Console.Write("Date: "+date);
                            record.date = date;
                            if (String.IsNullOrEmpty(record.fName) || String.IsNullOrEmpty(record.city) || String.IsNullOrEmpty(record.country) || String.IsNullOrEmpty(record.email) || String.IsNullOrEmpty(record.lName)
                                || String.IsNullOrEmpty(record.phonenum) || String.IsNullOrEmpty(record.postalcode) || String.IsNullOrEmpty(record.province) || String.IsNullOrEmpty(record.street) || String.IsNullOrEmpty(record.streetnum))
                            {
                                count++;
                                removeFromList.Add(record);
                            }
                            else
                            {
                                countRows++;
                            }
                        }
                        List<CustomerInfo> tmpList = recordList.Except(removeFromList).ToList();
                        globalList.AddRange(tmpList);
                    }
                }
                catch (FileNotFoundException)
                {
                    log.Error("The file cannot be found.");
                }
            }

            public static void Main(String[] args)
            {
                DirectoryWalker fw = new DirectoryWalker();
                log4net.Config.XmlConfigurator.Configure();
                log.Info("***********CSV Reader Program****************");
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                fw.walk(@"/Users/shreerag/MCDA/MCDA5510/A00466805_MCDA5510_DOTNET/Sample Data");
                writeToCSV();
                watch.Stop();
                log.Info("Total Duration: " + watch.ElapsedMilliseconds + " ms");
                log.Info("--------------------------------------------------------------");
                log.Info("Skipped Rows: " + count);
                log.Info("--------------------------------------------------------------");
                log.Info("Total Rows: " + countRows);
            }

            private static void writeToCSV()
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    NewLine = Environment.NewLine,
                    MissingFieldFound = null
                };
                try
                {
                    using (var sw = new StreamWriter(@"/Users/shreerag/MCDA/MCDA5510/A00466805_MCDA5510_DOTNET/Assignment1/Assignment1/ProgAssign1/Output/finalFile.csv", true))
                    {
                        var writer = new CsvWriter(sw, config);
                        writer.WriteRecords(globalList);
                    }
                }
                catch (DriveNotFoundException)
                {
                    log.Error("The drive specified in 'path' is invalid.");
                }
                catch (DirectoryNotFoundException)
                {
                    log.Error("The directory cannot be found.");
                }
            }
    }
}
