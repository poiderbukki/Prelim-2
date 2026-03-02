# Phase 1: Design Blueprint — Eco-Grid Energy Distributor

## 1. UML Class Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│                        PowerSource (Base Class)                  │
├─────────────────────────────────────────────────────────────────┤
│ - sourceId: string          (private)                            │
│ - baseOutput: double        (private)                            │
├─────────────────────────────────────────────────────────────────┤
│ + SourceID: string          (public property)                    │
│ + BaseOutput: double        (public property)                   │
├─────────────────────────────────────────────────────────────────┤
│ + PowerSource(string, double)  (public constructor)             │
│ + GenerateReport(): string  (public virtual - summary)           │
│ + GenerateReport(bool): string (public virtual - overload)       │
└─────────────────────────────────────────────────────────────────┘
                                    △
                                    │ inherits
                    ┌───────────────┴───────────────┐
                    │                               │
┌───────────────────┴───────────┐   ┌───────────────┴───────────────┐
│      SolarPanel (Sub-Class)    │   │    WindTurbine (Sub-Class)    │
├───────────────────────────────┤   ├───────────────────────────────┤
│ - sunlightPercent: double     │   │ - windSpeed: double           │
│   (private)                   │   │   (private)                   │
├───────────────────────────────┤   ├───────────────────────────────┤
│ + SunlightPercent: double     │   │ + WindSpeed: double            │
│   (public property)           │   │   (public property)           │
├───────────────────────────────┤   ├───────────────────────────────┤
│ + SolarPanel(string, double,  │   │ + WindTurbine(string, double,  │
│   double)                     │   │   double)                     │
├───────────────────────────────┤   ├───────────────────────────────┤
│ + GenerateReport(): string    │   │ + GenerateReport(): string    │
│   (override)                  │   │   (override)                  │
│ + GenerateReport(bool): string│   │ + GenerateReport(bool): string│
│   (override)                  │   │   (override)                 │
└───────────────────────────────┘   └───────────────────────────────┘
```

**Member Visibility Summary:**
| Member           | Visibility | Description                          |
|------------------|------------|--------------------------------------|
| sourceId         | private    | Unique identifier for power source   |
| baseOutput       | private    | Base energy output in kW              |
| SourceID         | public     | Property with validation              |
| BaseOutput       | public     | Property with validation (≥ 0)        |
| SunlightPercent  | public     | Solar modifier (0–100)                |
| WindSpeed        | public     | Wind modifier (km/h)                  |
| GenerateReport() | public     | Virtual/override methods              |

---

## 2. Logic Flow — OOP Pillars in Eco-Grid

### Encapsulation
- All fields (`sourceId`, `baseOutput`, `sunlightPercent`, `windSpeed`) are **private**.
- Access is through **public properties** with validation in the `set` accessor.
- Example: Setting `BaseOutput = -5` throws `ArgumentException` before the invalid value is stored.

### Inheritance
- `SolarPanel` and `WindTurbine` inherit from `PowerSource`.
- Sub-class constructors use `: base(sourceId, baseOutput)` to initialize parent fields.
- Shared members (`SourceID`, `BaseOutput`, `GenerateReport`) are defined once in the base class.

### Polymorphism
- `GenerateReport()` is **virtual** in `PowerSource` and **override** in sub-classes.
- **SolarPanel**: Output = BaseOutput × (SunlightPercent / 100). Report includes sunlight data.
- **WindTurbine**: Output = BaseOutput × (WindSpeed / 25). Report includes wind data.
- A `PowerSource` reference can point to either sub-class; calling `GenerateReport()` uses the correct implementation at runtime.

### Abstraction
- `PowerSource` defines the common interface (properties and `GenerateReport`).
- Sub-classes provide concrete implementations for different energy sources.

---

## 3. Exception Mapping — Try-Catch Scenarios

| # | Scenario | Location | Exception | Why Try-Catch? |
|---|----------|----------|-----------|----------------|
| 1 | **Negative weather input** | Property setters (`SunlightPercent`, `WindSpeed`) | `ArgumentException` | User enters -10% sunlight or -5 km/h wind. Validation in `set` throws; caller must catch to avoid crash. |
| 2 | **Invalid BaseOutput** | Property setter (`BaseOutput`) | `ArgumentException` | User configures a source with negative base output. Prevents invalid power source configuration. |
| 3 | **Main execution** | `Program.Main()` | Any unhandled exception | Wraps all demo logic. Ensures "System Shutdown" prints in `finally` even if an exception occurs. |

**Code locations for try-catch:**
1. **Around property usage** — When creating `SolarPanel`/`WindTurbine` with user or external input.
2. **Around Main logic** — Wrapping the entire execution so the application exits cleanly with a shutdown message.
