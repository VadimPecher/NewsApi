using Models;

namespace Logic;

public class MeasurementCalculator : IMeasurementCalculator
{
    private const int MIN_TEMP = 31, MAX_TEMP = 42;
    private const int MIN_HR = 25, MAX_HR = 220;
    private const int MIN_RR = 3, MAX_RR = 60;

    public int Calculate(Measurement measurement)
    {
        var value = measurement.Value;
        switch (measurement.Type)
        {
            case MeasurementType.TEMP:
                if (value <= MIN_TEMP || value > MAX_TEMP) {
                    throw new ArgumentOutOfRangeException(nameof(measurement.Value), $"The TEMP value must be between {MIN_TEMP} and {MAX_TEMP}");
                }
                else if (value <= 35) return 3;
                else if (value <= 36) return 1;
                else if (value <= 38) return 0;
                else if (value <= 39) return 1;
                else if (value <= 42) return 2;
                break;

            case MeasurementType.HR:
                if (value <= MIN_HR || value > MAX_HR) {
                    throw new ArgumentOutOfRangeException(nameof(measurement.Value), $"The HR value must be between {MIN_HR} and {MAX_HR}");
                }
                else if (value <= 40) return 3;
                else if (value <= 50) return 1;
                else if (value <= 90) return 0;
                else if (value <= 110) return 1;
                else if (value <= 130) return 2;
                else if (value <= 220) return 3;
                break;

            case MeasurementType.RR:
                if (value <= MIN_RR || value > MAX_RR) {
                    throw new ArgumentOutOfRangeException(nameof(measurement.Value), $"The RR value must be between {MIN_RR} and {MAX_RR}");
                }
                else if (value <= 8) return 3;
                else if (value <= 11) return 1;
                else if (value <= 20) return 0;
                else if (value <= 24) return 2;
                else if (value <= 60) return 3;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(measurement.Type));
        }
        throw new NotImplementedException("Unexpexted exception during measurement calculation");
    }
}