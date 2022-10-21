using System;
using System.IO;


namespace Assignment1
{
  

    public class DirWalker 
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void walk(String path)
        {

            string[] list = Directory.GetDirectories(path);


            if (list == null) return;

            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath);
                    Console.WriteLine("Dir:" + dirpath);
                    log.Info("Dir: " + dirpath);
                }
            }
            string[] fileList = Directory.GetFiles(path);
            foreach (string filepath in fileList)
            {

                    Console.WriteLine("File:" + filepath);
                log.Info("File: " + filepath);
            }
        }

        //public static void Main(String[] args)
        //{
        //    DirWalker fw = new DirWalker();
        //    log4net.Config.XmlConfigurator.Configure();
        //    log.Info("Hello world");
        //    fw.walk(@"/Users/shreerag/MCDA/MCDA5510/A00466805_MCDA5510_DOTNET/Sample Data");
        //}

    }
}
