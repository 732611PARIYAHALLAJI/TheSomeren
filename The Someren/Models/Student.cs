namespace The_Someren.Models
{
    public class Student
    {
        public Student(int studentID, int roomID, string studentNumber, string name, string lastName, int age, string phoneNumber, string @class)
        {
            StudentID = studentID;
            RoomID = roomID;
            StudentNumber = studentNumber;
            Name = name;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Class = @class;
            Age = age;
        }
        public Student()
        {
            
        }

        public int StudentID { get; set; }  // کلید اصلی
        public int RoomID { get; set; }  // کلید خارجی به ROOM
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }


        public string PhoneNumber { get; set; }
        public string Class { get; set; }

        // ارتباط با Room
        public Room Room { get; set; }






    }

}
