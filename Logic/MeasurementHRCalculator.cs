using Models;

namespace Logic;

public class MeasurementHRCalculator : IMeasurementCalculator
{
    private const int MIN_VALUE = 25, MAX_VALUE = 220;

    public int Calculate(Measurement measurement)
    {
        MeasurementValidator.ValidateMinMaxValues(measurement, MIN_VALUE, MAX_VALUE);
        
        var value = measurement.Value;
        
        if (value <= 40) return 3;
        if (value <= 50) return 1;
        if (value <= 90) return 0;
        if (value <= 110) return 1;
        if (value <= 130) return 2;
        return 3;
    }

}