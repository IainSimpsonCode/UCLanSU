using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace SU_Course_Rep_Training_Software
{
    public class QuizEmail
    {
        public string rawData;
        public string emailAddress;
        public string dateCompleted;
        public bool matchFound;
        public QuizEmail(string email, string date, string data)
        {
            emailAddress = email;
            dateCompleted = date;
            rawData = data;
            matchFound = false;
        }
    }

    public class CSVFile
    {
        const char kDelimiter = ',';
        private int ArraySize = 0;
        private string FilePath = "";
        public List<List<string>> ImportedData = new List<List<string>>();

        public string Name = "";

        public CSVFile(string name, string filePath, int arraySize)
        {
            Name = name;
            FilePath = filePath;
            ArraySize = arraySize;
            Import();
        }

        public void Import()
        {
            ImportedData.Clear();

            StreamReader sr = new StreamReader(FilePath);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                List<string> rowList = new List<string>();
                string[] row = new string[ArraySize];
                row = line.Split(',');
                foreach (string rowItem in row)
                {
                    rowList.Add(rowItem);
                }
                ImportedData.Add(rowList);
            }

            sr.Close();
        }

        public void Export()
        {
            string exportFilePath = FilePath.Substring(0, FilePath.Length - 4) + " UPDATED.csv";
            StreamWriter sr = new StreamWriter(exportFilePath);

            foreach (List<string> line in ImportedData)
            {
                string exportLine = "";

                foreach (string lineItem in line)
                {
                    exportLine += lineItem;
                    exportLine += ",";
                }

                exportLine = exportLine.Substring(0, exportLine.Length); // remove trailing comma
                sr.WriteLine(exportLine); // output line to file
            }

            sr.Close();
        }

        public void PrintRow(int rowIndex)
        {
            foreach (string item in ImportedData[rowIndex])
            {
                Console.Write(item + ", ");
            }
            Console.Write("\n");
        }
    }
}
