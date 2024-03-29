﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeather.Model;
using System.Net.Http;
using System.Xml.Linq;

namespace OpenWeather.HelperClass
{
    static class Weather
    {
        private static string AppID = "f71587734a86b24d6832765c09fedbd9";

        public static async Task<List<WeatherDetails>> GetWeather(string location)
        {
            string url = string.Format
                ("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&type=accurate&mode=xml&units=metric&cnt=3&appid={1}", 
                location, AppID);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(url);
                    if (!(response.Contains("message") && response.Contains("cod")))
                    {
                        XElement xEl = XElement.Load(new System.IO.StringReader(response));
                        return GetWeatherInfo(xEl);
                    }
                    else
                    {
                        return new List<WeatherDetails>();
                    }
                }
                catch (HttpRequestException)
                {
                    return new List<WeatherDetails>();
                }
            }
        }

        private static List<WeatherDetails> GetWeatherInfo(XElement xEl)
        {
            IEnumerable<WeatherDetails> w = xEl.Descendants("time").Select((el) =>
                new WeatherDetails
                {
                    Humidity = el.Element("humidity").Attribute("value").Value + "%",
                    MaxTemperature = el.Element("temperature").Attribute("max").Value + "°",
                    MinTemperature = el.Element("temperature").Attribute("min").Value + "°",
                    Temperature = el.Element("temperature").Attribute("day").Value + "°",
                    Weather = el.Element("symbol").Attribute("name").Value,
                    WeatherDay = DayOfTheWeek(el),
                    WeatherIcon = WeatherIconPath(el),
                    WindDirection = el.Element("windDirection").Attribute("name").Value,
                    WindSpeed = el.Element("windSpeed").Attribute("mps").Value + "mps"
                });

            return w.ToList();
        }

        private static string DayOfTheWeek(XElement el)
        {
            DayOfWeek dW = Convert.ToDateTime(el.Attribute("day").Value).DayOfWeek;
            return dW.ToString();
        }

        private static string WeatherIconPath(XElement el)
        {
            string symbolVar = el.Element("symbol").Attribute("var").Value;
            string symbolNumber = el.Element("symbol").Attribute("number").Value;
            string dayOrNight = symbolVar.ElementAt(2).ToString(); // d or n
            return String.Format("WeatherIcons/{0}{1}.png", symbolNumber, dayOrNight);
        }
    }
}
