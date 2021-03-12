using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ProblemaLocação
{
    class Location
    {
        public string CarModel { get; private set; }
        public DateTime InitialDate { get; private set; }
        public DateTime FinalDate { get; private set; }
        public double HourValue { get; private set; }
        public double DaylyValue { get; private set; }
        public TimeSpan DurationTime { get; set; }
        public double TaxesValue { get; private set; }

        public Location(string carModel, DateTime initialDate, DateTime finalDate, double hourValue, double daylyValue)
        {
            CarModel = carModel;
            InitialDate = initialDate;
            FinalDate = finalDate;
            HourValue = hourValue;
            DaylyValue = daylyValue;
        }

        public TimeSpan Duration()
        {
            DurationTime = FinalDate.Subtract(InitialDate);
            return DurationTime;
        }

        public double Values()
        {
            
 
            if (DurationTime.Hours < 12 && DurationTime.Days < 1)
            {
                return HourValue * Math.Ceiling(DurationTime.TotalHours);
            }
            else
            {
                return DaylyValue * Math.Ceiling(DurationTime.TotalHours);
            }
        }

        public double ValueWithTaxes()
        {
            if (Values() < 100.00)
            {
                TaxesValue = 0.2 * Values();
                return Values() + TaxesValue;
            }
            else if (Values() > 100.00)
            {
                TaxesValue = 0.15 * Values();
                return Values() + TaxesValue;
            }
            else
            {
                return 0;
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("RENT INFO:");
            sb.AppendLine("Car model: " + CarModel);
            sb.AppendLine("Rent duration: " + DurationTime);
            sb.AppendLine("Total payied: " + ValueWithTaxes().ToString("F2", CultureInfo.InvariantCulture));
            sb.AppendLine("");
            sb.AppendLine("Thanks for the preference!");

            return sb.ToString();
        }
    }
}
