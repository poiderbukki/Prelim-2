using System;

namespace EcoGrid_ScenarioC;

/// <summary>
/// Base class for all power sources in the Eco-Grid energy distribution system.
/// </summary>
public class PowerSource
{
    private string _sourceId;
    private double _baseOutput;

    public string SourceID
    {
        get => _sourceId;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Source ID cannot be null or empty.", nameof(value));
            _sourceId = value;
        }
    }

    public double BaseOutput
    {
        get => _baseOutput;
        set
        {
            if (value < 0)
                throw new ArgumentException("Base output cannot be negative.", nameof(value));
            _baseOutput = value;
        }
    }

    public PowerSource(string sourceId, double baseOutput)
    {
        SourceID = sourceId;
        BaseOutput = baseOutput;
    }

    /// <summary>
    /// Generates a summary report.
    /// </summary>
    public virtual string GenerateReport()
    {
        return $"[Summary] Source: {SourceID} | Base Output: {BaseOutput:F2} kW";
    }

    /// <summary>
    /// Overload: Generates either a summary or detailed technical breakdown.
    /// </summary>
    /// <param name="detailed">True for detailed breakdown, false for summary.</param>
    public virtual string GenerateReport(bool detailed)
    {
        return detailed
            ? $"[Detailed] Source: {SourceID}\n  Base Output: {BaseOutput:F2} kW\n  Type: PowerSource"
            : GenerateReport();
    }
}

/// <summary>
/// Solar panel power source with sunlight percentage modifier.
/// </summary>
public class SolarPanel : PowerSource
{
    private double _sunlightPercent;

    public double SunlightPercent
    {
        get => _sunlightPercent;
        set
        {
            if (value < 0)
                throw new ArgumentException("Sunlight percentage cannot be negative.", nameof(value));
            if (value > 100)
                throw new ArgumentException("Sunlight percentage cannot exceed 100.", nameof(value));
            _sunlightPercent = value;
        }
    }

    public SolarPanel(string sourceId, double baseOutput, double sunlightPercent)
        : base(sourceId, baseOutput)
    {
        SunlightPercent = sunlightPercent;
    }

    /// <summary>
    /// Effective output = BaseOutput * (SunlightPercent / 100)
    /// </summary>
    public double GetEffectiveOutput()
    {
        return BaseOutput * (SunlightPercent / 100.0);
    }

    public override string GenerateReport()
    {
        return $"[Summary] Solar Panel {SourceID} | Output: {GetEffectiveOutput():F2} kW (Sunlight: {SunlightPercent}%)";
    }

    public override string GenerateReport(bool detailed)
    {
        if (!detailed)
            return GenerateReport();

        return $"[Detailed] Solar Panel - {SourceID}\n" +
               $"  Base Output: {BaseOutput:F2} kW\n" +
               $"  Sunlight Modifier: {SunlightPercent}%\n" +
               $"  Effective Output: {GetEffectiveOutput():F2} kW\n" +
               $"  Formula: BaseOutput × (SunlightPercent / 100)";
    }
}

/// <summary>
/// Wind turbine power source with wind speed modifier.
/// </summary>
public class WindTurbine : PowerSource
{
    private double _windSpeed;

    public double WindSpeed
    {
        get => _windSpeed;
        set
        {
            if (value < 0)
                throw new ArgumentException("Wind speed cannot be negative.", nameof(value));
            _windSpeed = value;
        }
    }

    public WindTurbine(string sourceId, double baseOutput, double windSpeed)
        : base(sourceId, baseOutput)
    {
        WindSpeed = windSpeed;
    }

    /// <summary>
    /// Effective output = BaseOutput * (WindSpeed / 25) — 25 km/h as nominal reference.
    /// </summary>
    public double GetEffectiveOutput()
    {
        return BaseOutput * (WindSpeed / 25.0);
    }

    public override string GenerateReport()
    {
        return $"[Summary] Wind Turbine {SourceID} | Output: {GetEffectiveOutput():F2} kW (Wind: {WindSpeed} km/h)";
    }

    public override string GenerateReport(bool detailed)
    {
        if (!detailed)
            return GenerateReport();

        return $"[Detailed] Wind Turbine - {SourceID}\n" +
               $"  Base Output: {BaseOutput:F2} kW\n" +
               $"  Wind Speed Modifier: {WindSpeed} km/h\n" +
               $"  Effective Output: {GetEffectiveOutput():F2} kW\n" +
               $"  Formula: BaseOutput × (WindSpeed / 25)";
    }
}

internal static class Program
{
    private static void Main()
    {
        try
        {
            Console.WriteLine("=== Eco-Grid Energy Distributor - Scenario C ===\n");

            // Demo: Solar Panel
            var solar = new SolarPanel("SOL-001", 100, 75);
            Console.WriteLine("Solar Panel Reports:");
            Console.WriteLine(solar.GenerateReport());
            Console.WriteLine(solar.GenerateReport(detailed: true));
            Console.WriteLine();

            // Demo: Wind Turbine
            var wind = new WindTurbine("WND-001", 50, 30);
            Console.WriteLine("Wind Turbine Reports:");
            Console.WriteLine(wind.GenerateReport());
            Console.WriteLine(wind.GenerateReport(detailed: true));
            Console.WriteLine();

            // Polymorphism: PowerSource reference
            PowerSource source = new SolarPanel("SOL-002", 80, 90);
            Console.WriteLine("Polymorphic call (Solar via PowerSource):");
            Console.WriteLine(source.GenerateReport());
            Console.WriteLine();

            // Exception demo: negative weather variable (ArgumentException)
            try
            {
                var badSolar = new SolarPanel("BAD", 50, -10);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nSystem Shutdown");
        }
    }
}
