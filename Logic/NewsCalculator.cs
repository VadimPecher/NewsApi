using Logic;

namespace Models;

public class NewsCalculator(IMeasurementCalculator measurementCalculator) : INewsCalculator
{
    private readonly MeasurementType[] RequiredMeasurements = [MeasurementType.TEMP, MeasurementType.HR, MeasurementType.RR];

    public NewsResult Calculate(IEnumerable<Measurement> measurements)
    {
        if (measurements.Count() > RequiredMeasurements.Length) {
            throw new ArgumentException($"The measurements count ({measurements.Count()}) is greater than expected ({RequiredMeasurements.Length})");
        }
        foreach (var requiredMeasurement in RequiredMeasurements)
        {
            if (measurements.FirstOrDefault(x => x.Type == requiredMeasurement) == null) {
                throw new ArgumentException($"The {requiredMeasurement} measurement was not provided");
            }
        }
        
        var results = Array.ConvertAll(measurements.ToArray(), m => new MeasurementResult(m.Type, measurementCalculator.Calculate(m)));
        return new NewsResult(results.Sum(x => x.Score), results);
    }
}