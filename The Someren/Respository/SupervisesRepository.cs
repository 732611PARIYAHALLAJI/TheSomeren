using Microsoft.Data.SqlClient;
using The_Someren.Models;
using The_Someren.Respository;

public class SupervisesRepository : ISupervisesRepository
{
    private readonly string? connectionString;

    public SupervisesRepository(IConfiguration configuration)
    {
        connectionString = "Server=tcp:testserverprojectpariya.database.windows.net,1433;Initial Catalog=TheSomeren;Persist Security Info=False;User ID=testadmin;Password=Pariya@4548;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }

    public void Add(Supervises supervises)
    {
        throw new NotImplementedException();
    }

    public void Delete(Supervises supervises)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Supervises WHERE LecturerID = @LecturerID AND ActivityID = @ActivityID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LecturerID", supervises.LecturerID);
                command.Parameters.AddWithValue("@ActivityID", supervises.ActivityID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }

    public List<Supervises> GetAll()
    {
        throw new NotImplementedException();
    }

    public Supervises? GetById(int supervisesId)
    {
        throw new NotImplementedException();
    }

    public void Update(Supervises supervises)
    {
        throw new NotImplementedException();
    }
}

