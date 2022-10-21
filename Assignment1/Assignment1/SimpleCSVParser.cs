using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.VisualBasic.FileIO;
using SearchOption = System.IO.SearchOption;

namespace Assignment1
{
    public class CustomerData
    {
        [Name("First Name")]
        public string fName { get; set; }
        [Name("Last Name")]
        public string lName { get; set; }
        [Name("Street Number")]
        public int streetnum { get; set; }
        [Name("Street")]
        public string street { get; set; }
        [Name("City")]
        public string city { get; set; }
        [Name("Province")]
        public string province { get; set; }
        [Name("Postal Code")]
        public string postalcode { get; set; }
        [Name("Country")]
        public string country { get; set; }
        [Name("Phone Number")]
        public string phonenum { get; set; }
        [Name("email Address")]
        public string email { get; set; }
        //public String date


    }

    public class SimpleCSVParser
    {


        public static void Main(String[] args)
        {
            SimpleCSVParser parser = new SimpleCSVParser();
            //parser.parse(@"/Users/shreerag/MCDA/MCDA5510/A00466805_MCDA5510_DOTNET/Assignment1/Assignment1/sampleFile.csv");
            //var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            //{
            //    NewLine = Environment.NewLine,
            //    //HasHeaderRecord = false
            //};
            String sourceDirectory = @"/Users/shreerag/MCDA/MCDA5510/A00466805_MCDA5510_DOTNET/Assignment1/Assignment1/CustomerData0.csv";
            //var csvFiles = Directory.EnumerateFiles(sourceDirectory, " *.csv", SearchOption.AllDirectories);
            readFile(sourceDirectory);
            //foreach (string currentFile in csvFiles)
            //{
            //    Console.WriteLine(currentFile);
            //    readFile(currentFile);
            //}
        }

        public static void readFile(string currentFile)
        {
            using (var sr = new StreamReader(currentFile))
            {
                using (var sw = new StreamWriter(@"/Users/shreerag/MCDA/MCDA5510/A00466805_MCDA5510_DOTNET/Assignment1/Assignment1/finalFile.csv", true))
                {
                    var reader = new CsvReader(sr,CultureInfo.InvariantCulture);
                    var writer = new CsvWriter(sw,CultureInfo.InvariantCulture);

                    //CSVReader will now read the whole file into an enumerable
                    var dataRecords = reader.GetRecords<CustomerData>();
                    writer.WriteRecords(dataRecords);
                    //Console.WriteLine(dataRecords.ToString());
                    //writer.WriteField(dataRecords);
                    //foreach (var record in dataRecords)
                    //{
                    //    //Choose which data values you want to keep
                    //    writer.WriteField(record);
                    //    Console.WriteLine(record);
                    //    //Moves the pointer onto the next record
                    //    writer.NextRecord();
                    //}

                }
            }
        }
                    public void parse(String fileName)
        {
            try { 
            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        Console.WriteLine(field);
                    }
                }
            }
        
        }catch(IOException ioe){
                Console.WriteLine(ioe.StackTrace);
         }

    }


    }
}
