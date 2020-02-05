
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Kalkulator.Models
{
    public class CurrencyChangeModel
    {
        [Display(Name = "Ilość")]
        [Required]
        public double Amount { get; set; }
        [Display(Name = "Konwersja  z:")]
        [Required]
        public Rate SellCode { get; set; }
        [Display(Name = "Konwersja na:")]
        [Required]
        public Rate BuyCode { get; set; }
        public double ConversionResoult { get; set; }
        public List<string> CurrencyList { get; set; }
        public CourrencyModel Currency { get; set; }

        public double ConvertCurrencies(double amount, List<Rate> source, string sellCode, string buyCode, string orginCode = "PLN")
        {
            double result;
            Rate sell = PickRateFromList(source, sellCode);
            Rate buy = PickRateFromList(source, buyCode);
            if (sellCode.Equals(orginCode))
            {
                result = ConvertFromOrgin(amount, buy);
            }
            else if (buyCode.Equals(orginCode))
            {
                result = ConvertToOrgin(amount, sell);
            }
            else
            {
                result = ConvertFromOrgin(ConvertToOrgin(amount, sell), buy);
            }
            return result;
        }
        public CourrencyModel GetCurrency()
        {
            HttpClient client = new HttpClient();
            string json = client.GetStringAsync("http://api.nbp.pl/api/exchangerates/tables/c?format=json").Result;
            return CourrencyModel.FromJson(json)[0];
        }
        public List<string> GetCurrencyNames(List<Rate> source)
        {
            List<string> result = new List<string>();
            foreach (var rate in source)
            {
                result.Add(rate.Currency);
            }
            return result;
        }
        public Rate PickRateFromList(List<Rate> source, string code)
        {
            Rate result = new Rate();
            foreach (var rate in source)
            {
                if (rate.Code.Equals(code))
                {
                    result = rate;
                }
            }
            return result;
        }

        private double ConvertToOrgin(double amount, Rate rate)
        {
            return Math.Round(amount * rate.Bid, 4);
        }
        private double ConvertFromOrgin(double amount, Rate rate)
        {
            return Math.Round(amount / rate.Ask, 4);
        }

    }
}