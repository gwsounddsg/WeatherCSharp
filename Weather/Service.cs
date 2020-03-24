using System;





namespace Weather
{
    public class Service
    {
        private readonly IWebCall _webCall;




        public Service()
        {
            _webCall = new WebCall();
            Startup();
        }


        public Service(IWebCall wc)
        {
            _webCall = wc;
            Startup();
        }


        internal void Startup()
        {
            Console.WriteLine("What's the zip code you want to get the weather for?");
            string zip = Console.ReadLine();

            // pull data
            _webCall.PullData(zip);

            // display weather
            double fahrinheit = _webCall.GetFahrenheit();
            Console.WriteLine("Temperature is {0:N2}F", fahrinheit);
        }
    }
}
