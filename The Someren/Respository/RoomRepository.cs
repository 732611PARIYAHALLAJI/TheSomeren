using Microsoft.Data.SqlClient;
using The_Someren.Models;
using The_Someren.Respository;

public class RoomRepository : IRoomRepository
{
    private readonly string? connectionString;

    public RoomRepository(IConfiguration configuration)
    {
        connectionString = "Server=tcp:testserverprojectpariya.database.windows.net,1433;Initial Catalog=TheSomeren;Persist Security Info=False;User ID=testadmin;Password=Pariya@4548;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;";
    }

    public void Add(Room room)
    {

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO ROOM (Building, RoomType, RoomNumber, Capacity) " +
                           "VALUES (@Building, @RoomType, @RoomNumber, @Capacity);" +
                           "SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Building", room.Building);
                command.Parameters.AddWithValue("@RoomType", room.RoomType);
                command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                command.Parameters.AddWithValue("@Capacity", room.Capacity);

                connection.Open();
                room.RoomID = Convert.ToInt32(command.ExecuteScalar());
            }
        }

    }

    public void Delete(Room room)
    {

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM ROOM WHERE RoomID = @RoomID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RoomID", room.RoomID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No records deleted!");
                }
            }
        }

    }

    public List<Room> GetAll()
    {

        List<Room> rooms = new List<Room>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT RoomID, Building, RoomType, RoomNumber, Capacity FROM ROOM ORDER BY RoomNumber";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Room room = new Room
                        {
                            RoomID = reader.GetInt32(0),
                            Building = reader.GetString(1),
                            RoomType = reader.GetString(2),
                            RoomNumber = reader.GetString(3),
                            Capacity = reader.GetInt32(4)
                        };
                        rooms.Add(room);
                    }
                }
            }
        }
        return rooms;

    }

    public Room? GetById(int roomId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT RoomID, Building , RoomType , RoomNumber ,Capacity FROM Room WHERE RoomID = @Id;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", roomId);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Room
                    {
                        RoomID = reader.GetInt32(0),
                        Building = reader.GetString(1),
                        RoomType = reader.GetString(2),
                        RoomNumber = reader.GetString(3),
                        Capacity = reader.GetInt32(4)
                    };
                }
            }
        }
        return null;

    }

    public void Update(Room room)
    {

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE ROOM SET Building = @Building, RoomType = @RoomType, " +
                           "RoomNumber = @RoomNumber, Capacity = @Capacity WHERE RoomID = @RoomID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RoomID", room.RoomID);
                command.Parameters.AddWithValue("@Building", room.Building);
                command.Parameters.AddWithValue("@RoomType", room.RoomType);
                command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                command.Parameters.AddWithValue("@Capacity", room.Capacity);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}

