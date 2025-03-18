namespace The_Someren.Models
{
    public class Participates
    {
        public int StudentID { get; set; }  // کلید خارجی به Student
        public int ActivityID { get; set; }  // کلید خارجی به Activity

        // ارتباط با جداول دیگر
        public Student Student { get; set; }
        public Activity Activity { get; set; }
    }
}
