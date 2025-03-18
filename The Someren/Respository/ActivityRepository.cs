using Microsoft.Data.SqlClient;
using The_Someren.Models;


namespace The_Someren.Respository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly string? connectionString;

        public ActivityRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("TheSomeren");
        }

        public void Add(Activity activity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO dbo.ACTIVITY (name, start_time, end_time)" +
                    "VALUES (@Name,@StartTime,@EndTime);" +
                    "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", activity.Name);
                command.Parameters.AddWithValue("@StartTime", activity.StartTime);
                command.Parameters.AddWithValue("@EndTime", activity.EndTime);
                connection.Open();
                activity.ActivityID = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Delete(Activity activity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Activity WHERE ActivityID = @Id;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", activity.ActivityID);

                connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();

                if (nrOfRowsAffected == 0)
                    throw new Exception("No records deleted!");
            }
        }

        public List<Activity> GetAll()
        {
            List<Activity> activities = new List<Activity>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Activityid, Name , Start_time , End_time FROM ACTIVITY ORDER BY Start_time";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Activity activity = ReadActivity(reader);
                            activities.Add(activity);
                        }
                    }
                }
            }

            return activities;
        }

        public Activity? GetById(int activityId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Activityid, Name , Start_time , End_time FROM ACTIVITY WHERE Activityid = @Id;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", activityId);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Activity
                        {
                            ActivityID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            StartTime = reader.GetDateTime(2),
                            EndTime = reader.GetDateTime(3)
                        };
                    }
                }
            }
            return null;
        }

        public void Update(Activity activity)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Activity SET Name = @Name, Start_time = @StartTime, " +
                               "End_time = @EndTime WHERE ActivityID = @Id;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", activity.ActivityID);
                command.Parameters.AddWithValue("@Name", activity.Name);
                command.Parameters.AddWithValue("@StartTime", activity.StartTime);
                command.Parameters.AddWithValue("@EndTime", activity.EndTime);


                connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();

                if (nrOfRowsAffected == 0)
                    throw new Exception("No records updated!");
            }

        }

        public Activity ReadActivity(SqlDataReader reader)
        {
            int activtyId = (int)reader["ActivityID"];
            DateTime start_time = (DateTime)reader["start_time"];
            DateTime end_time = (DateTime)reader["end_time"];
            string name = (string)reader["name"];

            return new Activity(activtyId, name, start_time, end_time);
        }




    }
}

