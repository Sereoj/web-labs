using System.ComponentModel.DataAnnotations;

namespace ProductCatalogApp.Attributes
{
    public class PositivePriceAttribute : ValidationAttribute
    {
        public PositivePriceAttribute() : base("Цена должна быть положительной.")
        {
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true; // Let Required attribute handle null values

            if (value is decimal price)
            {
                return price > 0;
            }

            if (value is double priceDouble)
            {
                return priceDouble > 0;
            }

            if (value is float priceFloat)
            {
                return priceFloat > 0;
            }

            if (value is int priceInt)
            {
                return priceInt > 0;
            }

            return false;
        }
    }
}