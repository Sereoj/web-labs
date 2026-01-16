using System.Text.Json;

namespace OrderManagementApp.Models
{
    public class Order
    {
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        
        public decimal GetTotalAmount()
        {
            return Items.Sum(item => item.GetTotalPrice());
        }
    }
    
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            session.SetString(key, json);
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var json = session.GetString(key);
            if (json == null)
            {
                return default(T);
            }
            
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}