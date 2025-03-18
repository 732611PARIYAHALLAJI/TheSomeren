using Microsoft.Data.SqlClient;
using The_Someren.Models;
using The_Someren.Respository;

public class DrinkRepository : IDrinkRepository
{
    private readonly string? connectionString;

    public DrinkRepository(IConfiguration configuration)
    {
        connectionString = "Server=tcp:testserverprojectpariya.database.windows.net,1433;Initial Catalog=TheSomeren;Persist Security Info=False;User ID=testadmin;Password=Pariya@4548;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }

    public void Add(Drink drink)
    {

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO DRINK (Name, VAT, Price, Stock) " +
                           "VALUES (@Name, @VAT, @Price, @Stock);" +
                           "SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", drink.Name);
                command.Parameters.AddWithValue("@VAT", drink.VAT);
                command.Parameters.AddWithValue("@Price", drink.Price);
                command.Parameters.AddWithValue("@Stock", drink.Stock);

                connection.Open();
                drink.DrinkID = Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }

    public void Delete(Drink drink)
    {

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM DRINK WHERE DrinkID = @DrinkID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DrinkID", drink.DrinkID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No records deleted!");
                }
            }
        }

    }

    public List<Drink> GetAll()
    {
        List<Drink> drinks = new List<Drink>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT DrinkID, Name, VAT, Price, Stock FROM DRINK";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Drink drink = new Drink
                        {
                            DrinkID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            VAT = reader.GetDouble(2),
                            Price = reader.GetDecimal(3),
                            Stock = reader.GetInt32(4)
                        };
                        drinks.Add(drink);
                    }
                }
            }
        }
        return drinks;

    }

    public Drink? GetById(int drinkId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT DrinkID, Name , VAT , Price ,Stock FROM Drink WHERE DrinkID = @Id;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", drinkId);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Drink
                    {
                        DrinkID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        VAT = reader.GetDouble(1),
                        Stock = reader.GetInt32(2),
                        Price = reader.GetDecimal(3)
                    };
                }
            }
        }
        return null;
    }

    public void Update(Drink drink)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE DRINK SET Name = @Name, VAT = @VAT, " +
                           "Price = @Price, Stock = @Stock WHERE DrinkID = @DrinkID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DrinkID", drink.DrinkID);
                command.Parameters.AddWithValue("@Name", drink.Name);
                command.Parameters.AddWithValue("@VAT", drink.VAT);
                command.Parameters.AddWithValue("@Price", drink.Price);
                command.Parameters.AddWithValue("@Stock", drink.Stock);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
    /*public Drink ReadDrink(SqlDataReader reader)
    {
        int drinkId = (int)reader["DrinkID"];
        double VAT = (double)reader["VAT"];
        decimal Price = (decimal)reader["Price"];
        string name = (string)reader["name"];
        int Stock = (int)reader["Stock"];


        return new Drink (drinkId, name, VAT, Price, Stock);
    }*/
}


