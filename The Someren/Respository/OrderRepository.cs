using Microsoft.Data.SqlClient;
using The_Someren.Models;
using The_Someren.Respository;

public class OrderRepository : IOrderRepository
{
    private readonly string? connectionString;

    public OrderRepository(IConfiguration configuration)
    {
        connectionString = "Server=tcp:testserverprojectpariya.database.windows.net,1433;Initial Catalog=TheSomeren;Persist Security Info=False;User ID=testadmin;Password=Pariya@4548;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }

    public void Add(Order order)
    {

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO ORDERS (StudentID, DrinkID, Amount, Date) " +
                           "VALUES (@StudentID, @DrinkID, @Amount, @Date);" +
                           "SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StudentID", order.StudentID);
                command.Parameters.AddWithValue("@DrinkID", order.DrinkID);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@Date", order.Date);

                connection.Open();
                order.OrderID = Convert.ToInt32(command.ExecuteScalar());
            }
        }

    }

    public void Delete(Order order)
    {
        //(int orderID)

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM ORDERS WHERE OrderID = @OrderID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderID", order.OrderID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No records deleted!");
                }
            }
        }

    }

    public List<Order> GetAll()
    {

        List<Order> orders = new List<Order>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT OrderID, StudentID, DrinkID, Amount, Date FROM ORDERS";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            OrderID = reader.GetInt32(0),
                            StudentID = reader.GetInt32(1),
                            DrinkID = reader.GetInt32(2),
                            Amount = reader.GetInt32(3),
                            Date = reader.GetDateTime(4)
                        };
                        orders.Add(order);
                    }
                }
            }
        }
        return orders;


    }

    public Order? GetById(int orderId)
    {

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT OrderID, StudentID , DrinkID , Amount, Date , FROM Order WHERE OrderID = @Id;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", orderId);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Order
                    {
                        OrderID = reader.GetInt32(0),
                        StudentID = reader.GetInt32(1),
                        DrinkID = reader.GetInt32(2),
                        Amount = reader.GetInt32(3),
                         Date = reader.GetDateTime(3)

                    };
                }
            }
        }
        return null;

    }

    public void Update(Order order)
    {

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE ORDERS SET StudentID = @StudentID, DrinkID = @DrinkID, " +
                           "Amount = @Amount, Date = @Date WHERE OrderID = @OrderID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderID", order.OrderID);
                command.Parameters.AddWithValue("@StudentID", order.StudentID);
                command.Parameters.AddWithValue("@DrinkID", order.DrinkID);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@Date", order.Date);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}

