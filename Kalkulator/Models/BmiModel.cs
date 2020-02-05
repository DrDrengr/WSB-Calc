using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Kalkulator.Models
{
    public class BmiModel
    {
        [RegularExpression(@"\b(([1-9]{1}[0-9]{1,2}\.[0-9]{1,2})|([1-9]{1}[0-9]{1,2}\,[0-9]{1,2})|([1-9]{1}[0-9]{1,2}))\b",
            ErrorMessage = "Dozwolone znaki to [0-9 oraz , i .] Podaj własciwą wagę, w kg!")]
        [Required]
        [MinLength(2, ErrorMessage = "Pole o minimalnej długości 2 znaki")]
        [MaxLength(6, ErrorMessage = "Pole o maksymalnej długości 6 znaków")]
        [Display(Name = "Waga [kg]")]
        public string Weight { get; set; }
        
        [RegularExpression(@"\b([1-4]{1}[0-9]{2})|([1-9]{1}[0-9]{1})\b",
            ErrorMessage = "Dozwolone znaki to [0-9] Podaj właściwy wzrost, w cm!")]
        [Required]
        [MinLength(2, ErrorMessage = "Pole o minimalnej długości 2 znaki")]
        [MaxLength(3, ErrorMessage = "Pole o maksymalnej długości 3 znaki")]
        [Display(Name = "Wzrost [cm]")]
        public string Height { get; set; }
        [Display(Name = "BMI")]
        public double Bmi { get; set; }

        public static double GetBmi(double weight, double height)
        {
            height /= 100;
            return Math.Round(weight / (height * height),2);
        }

        public static string[] GetBmiText(double bmi)
        {
            string[] result = new string[3];

            if (bmi > 0 && bmi < 18.5)
            {
                if (bmi > 0 && bmi < 16)
                {
                    result[0] = "wygłodzenie";
                    result[1] = "#082E79";
                    result[2] = "white";
                }
                else if (bmi >= 16 && bmi < 17)
                {
                    result[0] = "wychudzenie";
                    result[1] = "#4169E1";
                    result[2] = "black";
                }
                else if (bmi >= 17 && bmi < 18.5)
                {
                    result[0] = "niedowaga";
                    result[1] = "#ACE1AF";
                    result[2] = "black";
                }
            }
            else if (bmi >= 18.5 && bmi < 30)
            {
                if (bmi >= 18.5 && bmi < 25)
                {
                    result[0] = "pożądana masa ciała";
                    result[1] = "#CDEBA7";
                    result[2] = "black";
                }
                else if (bmi >= 25 && bmi < 30)
                {
                    result[0] = "nadwaga";
                    result[1] = "#FFFF99";
                    result[2] = "black";
                }
            }
            else if (bmi >= 30)
            {
                if (bmi >= 30 && bmi < 35)
                {
                    result[0] = "otyłość I stopnia";
                    result[1] = "#FDE456";
                    result[2] = "black";
                }
                else if (bmi >= 35 && bmi < 40)
                {
                    result[0] = "otyłość II stopnia (duża)";
                    result[1] = "#CF2929";
                    result[2] = "black";
                }
                else if (bmi >= 40)
                {
                    result[0] = "otyłość III stopnia (chorobliwa)";
                    result[1] = "#801818";
                    result[2] = "white";
                }
            }
            else
            {
                result[0] = "Błędny format";
                result[1] = "#801818";
                result[2] = "white";
            }
            return result;
        }
    }
}