namespace The_Someren.Models
{
    public class Order
    {
        public int OrderID { get; set; }  // کلید اصلی
        public int StudentID { get; set; }  // کلید خارجی به Student
        public int DrinkID { get; set; }  // کلید خارجی به Drink
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        // ارتباط با جداول دیگر
        public Student Student { get; set; }
        public Drink Drink { get; set; }
    }
}
