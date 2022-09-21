using System;
using System.IO;

namespace DatabaseSQLParser
{
    class Program
    {
        // TODO: Megírni hogy a user adja meg az file elérési útvonalát.
        static readonly string textFile = @"G:\Downloads\e_inffor_20okt_fl\3_Kozossegi_szolgalat\munka.txt";
        static string insertQuery = "INSERT INTO ";
        static void Main(string[] args)
        {
            insertQuery += FindTableName(textFile) + " (";
            //Read Input File
            string[] lines = File.ReadAllLines(textFile);
            //Split Header
            string[] splittedFirstRow = lines[0].Split('\t');
            for (int i = 0; i < splittedFirstRow.Length; i++)
            {
                insertQuery += splittedFirstRow[i];
                if(i == splittedFirstRow.Length - 1)
                {
                    insertQuery += ") ";
                }
                else
                {
                    insertQuery += ", ";
                }
            }

            insertQuery += "VALUES ";

            //Split Values
            for (int i = 1; i < lines.Length; i++)
            {
                string[] splittedRow = lines[i].Split('\t');
                insertQuery += "(";

                for (int j = 0; j < splittedRow.Length; j++)
                {
                    CheckType(splittedRow[j]);
                    
                    if (j < splittedRow.Length - 1)
                    {
                        insertQuery += ", ";
                    }
                }
                if(i == lines.Length - 1)
                {
                    insertQuery += ");";
                }
                else
                insertQuery += "), ";
            }
            Console.WriteLine(insertQuery);
            File.WriteAllText(@"G:\Downloads\e_inffor_20okt_fl\3_Kozossegi_szolgalat\munkaSQL.txt", insertQuery);
            Console.WriteLine("Write was successful");
        }

        //Check if varchar/int/double
        public static void CheckType(string item)
        {
            if (int.TryParse(item, out int numValue))
            {
                insertQuery += numValue.ToString();
            }
            else if (double.TryParse(item, out double dValue))
            {
                insertQuery += dValue.ToString();
            }
            else
            {
                insertQuery += "'" + item + "'";
            }
        }

        //Find table name
        public static string FindTableName(string fileLocation)
        {
            string s = @"\";
            string[] a  = fileLocation.Split(s);
            s = ".";
            a = a[a.Length - 1].Split(s);
            return a[0];
        }
    }
}
