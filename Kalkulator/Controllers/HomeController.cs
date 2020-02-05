using Kalkulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace Kalkulator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Strona główna";
            return View();
        }
        [HttpGet]
        public ActionResult Bmi()
        {
            BmiModel model = new BmiModel();
            ViewBag.Title = "BMI";
            return View("Bmi", model);
        }
        [HttpPost]
        public ActionResult Bmi(BmiModel model)
        {
            ViewBag.Title = "BMI";
            bool licz = true;
            double weight = 0;
            double height = 0;
            try
            {
                weight = double.Parse(model.Weight.Replace('.', ','));
                height = double.Parse(model.Height.Replace('.', ','));
            }
            catch (Exception)
            {
                licz = false;
            }
            if (licz)
            {
                model.Bmi = BmiModel.GetBmi(weight, height);
                string[] strArr= BmiModel.GetBmiText(model.Bmi);
                ViewBag.BmiText = strArr[0].ToUpper();
                ViewBag.BmiBgColor = strArr[1];
                ViewBag.BmiFontColor = strArr[2];
            }
            return View("Bmi", model);
        }

        [HttpGet]
        public ActionResult CurrencyChange()
        {
            CurrencyChangeModel model = new CurrencyChangeModel();
            Rate ORGIN = new Rate() { Currency = "złoty", Code = "PLN", Ask = 1, Bid = 1 };
            model.Currency = model.GetCurrency();
            model.Currency.Rates.Insert(0, ORGIN);
            model.CurrencyList = model.GetCurrencyNames(model.Currency.Rates);
            ViewBag.OrginCourrency = ORGIN;
            ViewBag.Title = "Waluty";        
            return View("CurrencyChange", model);
        }

        [HttpPost]
        public ActionResult CurrencyChange(double amount, string sellCode, string buyCode)
        {
            CurrencyChangeModel model = new CurrencyChangeModel();
            Rate ORGIN = new Rate() { Currency = "złoty", Code = "PLN", Ask = 1, Bid = 1 };
            model.Currency = model.GetCurrency();
            model.Currency.Rates.Insert(0,ORGIN);
            model.Amount = amount;
            model.SellCode = model.PickRateFromList(model.Currency.Rates,sellCode);
            model.BuyCode = model.PickRateFromList(model.Currency.Rates, buyCode);
            model.CurrencyList = model.GetCurrencyNames(model.Currency.Rates);
            model.ConversionResoult = model.ConvertCurrencies(amount, model.Currency.Rates, sellCode, buyCode, ORGIN.Code);
            ViewBag.Title = "Waluty";
            return View("CurrencyChange", model);
        }
    }
}