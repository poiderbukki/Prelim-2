# Eco-Grid Energy Distributor — Scenario C

Basic “how to use” guide for your console app.

---

## 1. What this program does

- **Purpose**: Simulates an eco‑friendly power grid using two energy sources:
  - **SolarPanel** – uses sunlight percentage.
  - **WindTurbine** – uses wind speed.
- The app prints:
  - A **summary** report for each source.
  - A **detailed technical** report when requested.
  - A final message: **`System Shutdown`**.

---

## 2. Files you care about

- **`Student_ScenarioC.sln`**  
  The Visual Studio solution (you can rename it to `YourSurname_ScenarioC.sln` if needed).
- **`EcoGrid_ScenarioC/Program.cs`**  
  The main C# code with the `PowerSource`, `SolarPanel`, and `WindTurbine` classes.

You normally don’t need to edit other files to just run the app.

---

## 3. How to run it with Visual Studio

1. Open **Visual Studio**.
2. Go to **File → Open → Project/Solution…**.
3. Open:  
   `c:\Users\acer\Desktop\IT Elect\Prelim 2\Student_ScenarioC.sln`
4. In **Solution Explorer**, right‑click `EcoGrid_ScenarioC` → **Set as Startup Project** (if not already).
5. Press **F5** (Start Debugging).
6. A console window appears and shows the reports, then **`System Shutdown`**.

---

## 4. How to run it with PowerShell / Terminal

1. Open **PowerShell**.
2. Go to your project folder:

   ```powershell
   cd "c:\Users\acer\Desktop\IT Elect\Prelim 2"
   ```

3. Build the project:

   ```powershell
   dotnet build Student_ScenarioC.sln
   ```

4. If the build succeeds, run the app:

   ```powershell
   dotnet run --project EcoGrid_ScenarioC
   ```

5. Read the output in the console; it will end with:

   ```text
   System Shutdown
   ```

---

## 5. Changing the inputs (optional)

If you want to experiment:

- Open `Program.cs`.
- Look for the lines that create the objects:
  - `new SolarPanel("SOL-001", 100, 75);`
  - `new WindTurbine("WND-001", 50, 30);`
- Change the numbers (but **don’t use negative values**, or it will throw an `ArgumentException`).



I let ai design this readme

02/03/2026- I AM SO SORRY I THOUGHT I PASSED THIS ALREADY