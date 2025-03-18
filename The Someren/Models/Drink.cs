namespace The_Someren.Models
{
    public class Drink
    {
        public Drink(int drinkID, string name, double vAT, decimal price, int stock)
        {
            DrinkID = drinkID;
            Name = name;
            VAT = vAT;
            Price = price;
            Stock = stock;
        }
        public Drink()
        {
            
        }

        public int DrinkID { get; set; }  // کلید اصلی
        public string Name { get; set; }
        public double VAT { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // ارتباط با سفارشات
        public ICollection<Order> Orders { get; set; }
    }
}
