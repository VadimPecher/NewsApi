using Models;

public static class MeasurementValidator
{
    internal static void ValidateMinMaxValues(Measurement measurement, int min_value, int max_value)
    {
        if (measurement.Value <= min_value || measurement.Value > max_value) {
            throw new ArgumentOutOfRangeException(nameof(measurement.Value), $"The {measurement.Type} value must be between {min_value} and {max_value}");
        }
    }
}