namespace The_Someren.Models
{
    public class Activity
    {
        public Activity(int activityID, string name, DateTime startTime, DateTime endTime)
        {
            ActivityID = activityID;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
        }
        public Activity()
        {
            
        }

        public int ActivityID { get; set; }  // کلید اصلی
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // ارتباط با دانشجویان و اساتید
        public ICollection<Participates> Participants { get; set; }
        public ICollection<Supervises> Supervisors { get; set; }
    }
}
