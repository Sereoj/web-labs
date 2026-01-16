using System.ComponentModel.DataAnnotations;

namespace ProductCatalogApp.Attributes
{
    public class PositiveQuantityAttribute : ValidationAttribute
    {
        public PositiveQuantityAttribute() : base("Количество должно быть положительным.")
        {
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true; // Let Required attribute handle null values

            if (value is int quantity)
            {
                return quantity > 0;
            }

            if (value is decimal quantityDecimal)
            {
                return quantityDecimal > 0;
            }

            if (value is double quantityDouble)
            {
                return quantityDouble > 0;
            }

            if (value is float quantityFloat)
            {
                return quantityFloat > 0;
            }

            return false;
        }
    }
}