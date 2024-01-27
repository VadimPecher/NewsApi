using Logic;

namespace Models;

public class NewsCalculator(MeasurementType[] requiredMeasurements, IServiceProvider serviceProvider) : INewsCalculator
{
    public NewsResult Calculate(IEnumerable<Measurement> measurements)
    {
        if (measurements.Count() > requiredMeasurements.Length) {
            throw new ArgumentException($"The measurements count ({measurements.Count()}) is greater than expected ({requiredMeasurements.Length})");
        }
        foreach (var requiredMeasurement in requiredMeasurements)
        {
            if (measurements.FirstOrDefault(x => x.Type == requiredMeasurement) == null) {
                throw new ArgumentException($"The {requiredMeasurement} measurement was not provided");
            }
        }
        
        var results = Array.ConvertAll(measurements.ToArray(), m => new MeasurementResult(m.Type, 
            serviceProvider.GetRequiredKeyedService<IMeasurementCalculator>(m.Type).Calculate(m))); // using calculators from DI

        return new NewsResult(results.Sum(x => x.Score), results);
    }
}