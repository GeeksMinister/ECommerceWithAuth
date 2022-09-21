namespace DataAccessLibrary.Models;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Currency
{
    SEK,
    USD,
    EUR,
    GBP,
    CAD,
    CHF,
    JPY,
    NOK,
    DKK
}


