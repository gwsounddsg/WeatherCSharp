using System;



namespace Weather
{
    class Program
    {
        static void Main(string[] args)
        {
            string zipCode = "33573";

            Console.WriteLine("What's the zip code you want to get the weather for?");
            zipCode = Console.ReadLine();

            // length check
            if (zipCode.Length != 5)
            {
                Console.WriteLine("The zipcode you entered, {0}, is not the right length of 5 numbers", zipCode);
                return;
            }

            // type check
            if (!Int32.TryParse(zipCode, out _))
            {
                Console.WriteLine("The zipcode you entered, {0}, is not valid. It contains non-numeric characters", zipCode);
                return;
            }

            // get zip code
            WebCall myCall = new WebCall();
            myCall.PullData(zipCode);

            // display weather
            double fahrinheit = myCall.GetFahrenheit();
            Console.WriteLine("Temperature is {0:N2}F", fahrinheit);           
        }
    }
}
