namespace The_Someren.Models
{
    public class Lecturer
    {
        public Lecturer()
        {
            
        }
        public Lecturer(int lecturerID, int roomID, string name, string lastName, string phoneNumber, int age)
        {
            LecturerID = lecturerID;
            RoomID = roomID;
            Name = name;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Age = age;
        }

        public int LecturerID { get; set; }  // کلید اصلی
        public int RoomID { get; set; }  // کلید خارجی به ROOM
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

        // ارتباط با Room
        public Room Room { get; set; }
    }
}
