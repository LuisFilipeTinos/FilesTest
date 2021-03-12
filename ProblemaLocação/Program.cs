using System;
using System.Globalization;
using System.IO;


namespace ProblemaLocação
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            try
            {
                Console.WriteLine("Enter rental data");
                Console.Write("Car model: ");
                string carModel = Console.ReadLine();
                Console.Write("Pickup (dd/MM/yyyy hh:mm): ");
                DateTime initialDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Return (dd/MM/yyyy hh:mm): ");
                DateTime finalDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter price per hour: ");
                double hourValue = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.Write("Enter price per day: ");
                double daylyValue = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                Location location = new Location(carModel, initialDate, finalDate, hourValue, daylyValue);

                location.Values();
                location.Duration();
                location.ValueWithTaxes();


                Directory.CreateDirectory(path + @"\Out");
                string filePath = Path.Combine(path + @"\Out\rent.txt");

                Console.WriteLine();
                Console.WriteLine("INVOICE");
                Console.WriteLine("Basic payment: " + location.Values().ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine("Tax: " + location.TaxesValue.ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine("Total payment: " + location.ValueWithTaxes().ToString("F2", CultureInfo.InvariantCulture));

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(location);
                }

                Console.WriteLine();
                Console.ReadLine();
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occured!");
                Console.WriteLine(e.Message);
            }
            

        }
    }
}
