using Microsoft.Data.SqlClient;
using System.Data;
using The_Someren.Models;
using The_Someren.Respository;

public class LecturerRepository : ILecturerRepository
{
    private readonly string? connectionString;

    public LecturerRepository(IConfiguration configuration)
    {
        connectionString = "Server=tcp:testserverprojectpariya.database.windows.net,1433;Initial Catalog=TheSomeren;Persist Security Info=False;User ID=testadmin;Password=Pariya@4548;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;";
    }

    public void Add(Lecturer lecturer)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO LECTURER (Name, LastName, phonenumber, Age, RoomId) " +
                           "VALUES (@Name, @LastName, @PhoneNumber, @Age, @RoomId);" +
                           "SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", lecturer.Name);
                command.Parameters.AddWithValue("@RoomId", lecturer.RoomID);
                command.Parameters.AddWithValue("@LastName", lecturer.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", lecturer.PhoneNumber);
                command.Parameters.AddWithValue("@Age", lecturer.Age);

                connection.Open();
                lecturer.LecturerID = Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }


    public void Delete(Lecturer lecturer)
    {//ino id kardam

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Lecturer WHERE LecturerID = @LecturerID;";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LecturerID", lecturer.LecturerID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No records deleted!");
                }
            }
        }

    }

    public List<Lecturer> GetAll()
    {
        List<Lecturer> lecturers = new List<Lecturer>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT LecturerID, RoomID, Name, LastName, PhoneNumber, Age FROM Lecturer ORDER BY LastName;";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Lecturer lecturer = new Lecturer
                        {
                            LecturerID = reader.GetInt32(0),
                            RoomID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            LastName = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Age = reader.GetInt32(5)
                        };
                        lecturers.Add(lecturer);
                    }
                }
            }
        }
        return lecturers;
    }



    public Lecturer? GetById(int lectureId)
    {

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT LecturerID, Name , LastName, PhoneNumber , Age,RoomId FROM Lecturer WHERE LecturerID = @Id;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", lectureId);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Lecturer
                    {
                        LecturerID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Age = reader.GetInt32(4),
                        RoomID = reader.GetInt32(5)
                    };
                }
            }
        }
        return null;
    }



    public void Update(Lecturer lecturer)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Lecturer SET  RoomID = @RoomID, Name = @Name,   LastName = @LastName, PhoneNumber = @PhoneNumber,   Age = @Age WHERE LecturerID = @LecturerID;";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LecturerID", lecturer.LecturerID);
                command.Parameters.AddWithValue("@Name", lecturer.Name);
                command.Parameters.AddWithValue("@LastName", lecturer.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", lecturer.PhoneNumber);
                command.Parameters.AddWithValue("@Age", lecturer.Age);
                command.Parameters.AddWithValue("@RoomId", lecturer.RoomID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}

