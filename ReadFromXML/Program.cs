using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ReadFromXML
{
    class Customer
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Create the XML path file
            string path = Path.Combine(Environment.CurrentDirectory, @"Data.xml");
            //Load the XMl content from the path
            XDocument doc = XDocument.Load(path);
            //Store elements in List
            List<Customer> customers = doc.Root
                 .Elements("customer")
                 .Select(x => new Customer
                 {
                     Name = (string)x.Attribute("Name"),
                     Country = (string)x.Attribute("Country")
                 })
                 .ToList<Customer>();
            //Display the number of rows
            Console.WriteLine(customers.Count);
            
            //Create a stringbuilder
            var csv = new StringBuilder();
            
            //Display XML Content and add values to the stringbuilder
            foreach (var item in customers)
            {
                var Name = item.Name;
                var Country = item.Country;
                var newLine = string.Format("{0};{1}", Name, Country);
                csv.AppendLine(newLine);
                Console.WriteLine($"Name = {item.Name} \t Country={item.Country} ");
            }

            //Create the path where we want to save the CSV file
            string filepath = Path.Combine(Environment.CurrentDirectory, @"Data.csv");

            //Write data into the CSV path
            File.WriteAllText(filepath, csv.ToString(),Encoding.UTF8);
            Console.ReadLine();
        }
    }
}
