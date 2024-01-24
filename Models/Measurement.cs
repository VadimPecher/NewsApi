using System.Text.Json.Serialization;

namespace Models;

public record Measurement(MeasurementType Type, int Value) {}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MeasurementType
{
    TEMP = 0,
    HR = 1,
    RR = 2
}