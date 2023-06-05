namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            namespace agecalculator
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                DateTime dateTime = DateTime.Now;

                Console.WriteLine("Now welcome new texnology,\n " +
                    "which called AgeCalculator");

                Console.ReadKey();
                int year = Year();
                int month = Month();
                int day = Day();

                DateTime yourBirhday = new DateTime(year, month, day);
                TimeSpan difference = dateTime - yourBirhday;
                int days = (int)difference.TotalDays;
                int youryear = days / 365;
                int yearForWhile = youryear;
                int counter = 0;
                while (yearForWhile > 0)
                {
                    yearForWhile = yearForWhile - 4;
                    counter++;
                }
                int yourmonth = (days - counter) % 365 / 31;
                int yourday = (days - counter) % 365 % 31;
                Console.WriteLine($"Your Age: {youryear}yil {yourmonth}oy {yourday}kun ");

            }
            public static int Year()
            {
                Console.Clear();
                DateTime dateTime = DateTime.Now;
                Console.Write("Enter your birth year : ");
                int year = int.Parse(Console.ReadLine());
                while (year >= dateTime.Year)
                {
                    Console.Clear();
                    Console.WriteLine($"Entered year should be smaller than {dateTime.Year}");
                    Console.Write("\nEnter your birth year again: ");
                    year = int.Parse(Console.ReadLine());
                }
                return year;
            }

            public static int Month()
            {
                Console.Clear();
                Console.Write("Enter your birth Month : ");
                int Month = int.Parse(Console.ReadLine());
                while (Month > 12)
                {
                    Console.Clear();
                    Console.WriteLine("Entered month should be smaller or equal to 12 ");
                    Console.Write("\nEnter your birth month again: ");
                    Month = int.Parse(Console.ReadLine());
                }
                return Month;

            }

            public static int Day()
            {
                Console.Clear();
                Console.Write("\n Enter your birth day : ");
                int day = int.Parse(Console.ReadLine());
                while (day > 31)
                {
                    Console.Clear();
                    Console.WriteLine("Entered day should be smaller or equal to 31");
                    Console.Write("\nEnter your birth day again: ");
                    day = int.Parse(Console.ReadLine());
                }
                return day;
            }
        }
    }
}
    }
}