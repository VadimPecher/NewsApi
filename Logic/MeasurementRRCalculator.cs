using Models;

namespace Logic;

public class MeasurementRRCalculator : IMeasurementCalculator
{
    private const int MIN_VALUE = 3, MAX_VALUE = 60;

    public int Calculate(Measurement measurement)
    {
        MeasurementValidator.ValidateMinMaxValues(measurement, MIN_VALUE, MAX_VALUE);
        
        var value = measurement.Value;
        
        if (value <= 8) return 3;
        if (value <= 11) return 1;
        if (value <= 20) return 0;
        if (value <= 24) return 2;
        return 3;
    }

}