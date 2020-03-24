using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;





namespace Weather
{
    public class WebCall
    {
        // api.openweathermap.org/data/2.5/weather?zip={zip code}&appid={your api key}
        private string url = "http://api.openweathermap.org/data/2.5/weather?zip=";
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

            // parse into JObject
            zipData = JObject.Parse(content);
        }


        public string GetKelvin()
        {
            return GetKelvinAsString();
        }


        public double GetFahrenheit()
        {
            string str = GetKelvinAsString();
            if (str == "0") return 0.0;

            double kelvin = Convert.ToDouble(str);
            return (kelvin * (9.0/5.0)) - 459.67;
        }





        #region PrivateMethods

        private string GetKelvinAsString()
        {
            if (zipData == null) { return "0"; }
            return zipData["main"]["temp"].ToString();
        }

        #endregion PrivateMethods 
    }
}
