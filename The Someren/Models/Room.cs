namespace The_Someren.Models
{
    public class Room
    {
        public int RoomID { get; set; }  // کلید اصلی
        public string Building { get; set; }
        public string RoomType { get; set; } // "Student" یا "Lecturer"
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }

        // ارتباط با دانشجویان و اساتید
        public ICollection<Student> Students { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
    }
}
