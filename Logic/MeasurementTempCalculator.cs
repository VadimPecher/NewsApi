using Models;

namespace Logic;

public class MeasurementTempCalculator : IMeasurementCalculator
{
    private const int MIN_VALUE = 31, MAX_VALUE = 42;

    public int Calculate(Measurement measurement)
    {
        MeasurementValidator.ValidateMinMaxValues(measurement, MIN_VALUE, MAX_VALUE);
        
        var value = measurement.Value;

        if (value <= 35) return 3;
        if (value <= 36) return 1;
        if (value <= 38) return 0;
        if (value <= 39) return 1;
        return 2;
    }

}