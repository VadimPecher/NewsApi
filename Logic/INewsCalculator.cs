using Models;

namespace Logic;

public interface INewsCalculator
{
    NewsResult Calculate(IEnumerable<Measurement> measurements);
}