using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;





namespace Weather
{
    public class SearchResults
    {
        public string Temp { get; set; }
    }


    public class WebCall
    {
        // api.openweathermap.org/data/2.5/weather?zip={zip code}&appid={your api key}
        string url = "http://api.openweathermap.org/data/2.5/weather?zip=";

        private JObject zipData;


        public void PullData(string zipCode)
        {
            string myURL = url + zipCode + "&appid=" + Constants.WEATHER_KEY;
            
            // Create a request for the URL. 		
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(myURL);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();

            // read and convert to json
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            string json = JsonConvert.SerializeObject(content);

            // parse
            zipData = JObject.Parse(content);
        }


        public string GetKelvin()
        {
            if (zipData == null) { return "0"; }
            string str = zipData["main"]["temp"].ToString();
            return str;
        }


        public double GetFahrenheit()
        {
            if (zipData == null) { return 0.0; }
            string str = zipData["main"]["temp"].ToString();

            double kelvin = Convert.ToDouble(str);
            return (kelvin * (9.0/5.0)) - 459.67;
        }
    }
}
