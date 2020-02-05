using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kalkulator.Models
{
    public partial class CourrencyModel
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("no")]
        public string No { get; set; }

        [JsonProperty("tradingDate")]
        public DateTimeOffset TradingDate { get; set; }

        [JsonProperty("effectiveDate")]
        public DateTimeOffset EffectiveDate { get; set; }

        [JsonProperty("rates")]
        public List<Rate> Rates { get; set; }
    }

    public partial class Rate
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("bid")]
        public double Bid { get; set; }

        [JsonProperty("ask")]
        public double Ask { get; set; }
    }

    public partial class CourrencyModel
    {
        public static List<CourrencyModel> FromJson(string json) => JsonConvert.DeserializeObject<List<CourrencyModel>>(json, Kalkulator.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<CourrencyModel> self) => JsonConvert.SerializeObject(self, Kalkulator.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
