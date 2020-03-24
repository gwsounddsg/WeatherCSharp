using System;
namespace Weather
{
    public interface IWebCall
    {
        public void PullData(string zipCode);
        public string GetKelvin();
        public double GetFahrenheit();
    }
}
