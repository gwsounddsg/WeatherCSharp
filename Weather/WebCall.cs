﻿using System;
using System.IO;
using System.Net;

using Newtonsoft.Json.Linq;





namespace Weather
{
    public class WebCall: IWebCall
    {
        
        private string url = "http://api.openweathermap.org/data/2.5/weather?zip=";
        private JObject zipData;





        public void PullData(string zipCode) {
            if (!ZipCheck(zipCode)) { return; }

            string myURL = url + zipCode + "&appid=" + Constants.WEATHER_KEY;
            
            // Create a request for the URL. 		
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(myURL);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();

            // read and convert to json
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();

            // parse into JObject
            zipData = JObject.Parse(content);
        }


        public string GetKelvin()
        {
            return GetKelvinAsString();
        }


        public double GetFahrenheit() {
            string str = GetKelvinAsString();
            if (str == "0") {
                return 0.0;
            }

            double kelvin = Convert.ToDouble(str);
            return (kelvin * (9.0/5.0)) - 459.67;
        }





        #region PrivateMethods

        private bool ZipCheck(string zip)
        {
            // length check
            if (zip.Length != 5)
            {
                Console.WriteLine("The zipcode you entered, {0}, is not the right length of 5 numbers", zip);
                return false;
            }

            // type check
            if (!Int32.TryParse(zip, out _))
            {
                Console.WriteLine("The zipcode you entered, {0}, is not valid. It contains non-numeric characters", zip);
                return false;
            }

            return true;
        }

        private string GetKelvinAsString()
        {
            if (zipData == null) { return "0"; }
            return zipData["main"]["temp"].ToString();
        }

        #endregion PrivateMethods 
    }
}
