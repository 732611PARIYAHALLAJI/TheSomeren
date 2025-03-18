namespace The_Someren.Models
{
    public class Supervises
    {

        public int LecturerID { get; set; }  // کلید خارجی به Lecturer
        public int ActivityID { get; set; }  // کلید خارجی به Activity

        // ارتباط با جداول دیگر
        public Lecturer Lecturer { get; set; }
        public Activity Activity { get; set; }
    }
}
