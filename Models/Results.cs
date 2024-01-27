namespace Models;

public record NewsResult (int Score, MeasurementResult[] Results) {}
public record MeasurementResult(MeasurementType Type, int Score) { }