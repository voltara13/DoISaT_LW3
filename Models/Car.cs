using System;

namespace LW1.Models
{
    public class Car
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        public int Year { get; set; }

        public double EngineVolume { get; set; }

        public Guid Id { get; set; } = Guid.Empty;

        public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();

            if (string.IsNullOrWhiteSpace(Manufacturer))
            {
                validationResult.Append($"Manufacturer cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(Model))
            {
                validationResult.Append($"Model cannot be empty");
            }

            if (Year < 0)
            {
                validationResult.Append($"Year of issue cannot be negative");
            }

            if (EngineVolume < 0)
            {
                validationResult.Append($"Engine volume cannot be negative");
            }

            return validationResult;
        }

        public override string ToString()
        {
            return $"Manufacturer: {Manufacturer} | Model: {Model} | Year: {Year} | Engine volume: {EngineVolume}";
        }

    }
}
