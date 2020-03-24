using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;

using Newtonsoft.Json;





namespace Weather
{
    public class WebCall
    {
        // api.openweathermap.org/data/2.5/weather?zip={zip code}&appid={your api key}
        string url = "http://api.openweathermap.org/data/2.5/weather?zip=";

        public void call(string zipCode)
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

            // get temp data
            JsonTextReader parser = new JsonTextReader(new StringReader(json));
            while (parser.Read())
            {
                if (parser.Value != null)
                {
                    Console.WriteLine("Token: {0}, Value: {1}", parser.TokenType, parser.Value);
                }
                else
                {
                    Console.WriteLine("Token: {0}", parser.TokenType);
                }
            }

        }
    }


    public class InputGpioPort
    {
        [DataMember(Name = "pin")]
        public string pin { get; set; }

        [DataMember(Name = "gpio")]
        public string gpio { get; set; }

        [DataMember(Name = "value")]
        public string value { get; set; }
    }
}
