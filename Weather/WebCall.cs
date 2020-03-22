using System;
using System.IO;
using System.Net;
using System.Text;





namespace Weather
{
    public class WebCall
    {
        // api.openweathermap.org/data/2.5/weather?zip={zip code}&appid={your api key}
        string url = "api.openweathermap.org/data/2.5/weather?zip=";

        public void call(string zipCode)
        {
            string myURL = url + zipCode + "&appid=" + Constants.WEATHER_KEY;
            Console.WriteLine(myURL);

            // Create a request for the URL. 		
            WebRequest request = WebRequest.Create(myURL);

            // If required by the server, set the credentials.
            //request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Display the status.
            Console.WriteLine(response.StatusDescription);

            // clean up
            response.Close();
        }
    }
}
