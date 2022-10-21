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

namespace Assignment1
{
  

    public class DirWalker 
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static int count = 0;

        private static int countRows = 0;

        private static List<CustomerData> list = new List<CustomerData>();

        public void walk(String path)
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
                    readFile(filepath,filepath.Substring(68,10));
                }
                //log.Info("File: " + filepath);
                //81834
            }
        }

        public static void readFile(string currentFile,String date)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                MissingFieldFound = null
            };
            using (var sr = new StreamReader(currentFile))
            {
                //using (var sw = new StreamWriter(@"/Users/shreerag/MCDA/MCDA5510/A00466805_MCDA5510_DOTNET/Assignment1/Assignment1/ProgAssign1/Output/finalFile.csv", true))
                //{
                //    var reader = new CsvReader(sr, config);
                //    var writer = new CsvWriter(sw, config);

                //    //CSVReader will now read the whole file into an enumerable
                //    var dataRecords = reader.GetRecords<CustomerData>();
                //    var recordList = dataRecords.ToList();
                //    List<CustomerData> removeFromList = new List<CustomerData>();
                //    foreach (CustomerData record in recordList) {
                //        //Console.Write("Date: "+date);
                //        record.date = date;
                //        if (String.IsNullOrEmpty(record.fName) || String.IsNullOrEmpty(record.city) || String.IsNullOrEmpty(record.country) || String.IsNullOrEmpty(record.email) || String.IsNullOrEmpty(record.lName)
                //            || String.IsNullOrEmpty(record.phonenum) || String.IsNullOrEmpty(record.postalcode) || String.IsNullOrEmpty(record.province) || String.IsNullOrEmpty(record.street) || String.IsNullOrEmpty(record.streetnum))
                //        {
                //            count++;
                //            removeFromList.Add(record);
                //        }
                //        else {
                //            countRows++;
                //        }
                //    }
                //    //recordList.RemoveAll;
                //    List<CustomerData> list = recordList.Except(removeFromList).ToList();
                //    //Console.WriteLine(list)
                //    if (list.Count != 0)
                //    {
                //        list.RemoveAt(0);
                //    }
                //    writer.WriteRecords(list);

                //}
                var reader = new CsvReader(sr, config);
                var dataRecords = reader.GetRecords<CustomerData>();
                var recordList = dataRecords.ToList();
                List<CustomerData> removeFromList = new List<CustomerData>();
                foreach (CustomerData record in recordList)
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
                List<CustomerData> tmpList = recordList.Except(removeFromList).ToList();
                list.AddRange(tmpList);
            }
        }

        public static void Main(String[] args)
        {
            DirWalker fw = new DirWalker();
            log4net.Config.XmlConfigurator.Configure();
            log.Info("***********CSV Reader Program****************");
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            fw.walk(@"/Users/shreerag/MCDA/MCDA5510/A00466805_MCDA5510_DOTNET/Sample Data");
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                MissingFieldFound = null
            };
            using (var sw = new StreamWriter(@"/Users/shreerag/MCDA/MCDA5510/A00466805_MCDA5510_DOTNET/Assignment1/Assignment1/ProgAssign1/Output/finalFile.csv", true)) {
                var writer = new CsvWriter(sw, config);
                writer.WriteRecords(list);
            }
                watch.Stop();
            log.Info("Total Duration: " + watch.ElapsedMilliseconds + " ms");
            log.Info("--------------------------------------------------------------");
            log.Info("Skipped Rows: " + count);
            log.Info("--------------------------------------------------------------");
            log.Info("Total Rows: " + countRows);
        }

    }
}
