using The_Someren.Models;
using Microsoft.Data.SqlClient;
using System.Security.Claims;


namespace The_Someren.Respository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string? connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            connectionString = "Server=tcp:testserverprojectpariya.database.windows.net,1433;Initial Catalog=TheSomeren;Persist Security Info=False;User ID=testadmin;Password=Pariya@4548;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;";
        }
        public void Add(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Student (StudentNumber, Name, LastName, Age, PhoneNumber, Class, RoomID) " +
                   "VALUES(@StudentNumber, @Name, @LastName, @Age, @PhoneNumber, @Class, @RoomID)"
              + "SELECT SCOPE_IDENTITY() ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                    command.Parameters.AddWithValue("@Class", student.Class);
                    command.Parameters.AddWithValue("@RoomId", student.RoomID);

                    connection.Open();
                    student.StudentID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }


        public void Delete(Student Student)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Student WHERE StudentID = @StudentID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", Student.StudentID);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No records deleted!");
                    }
                }
            }

        }

        public List<Student> GetAll()
        {
            //2model
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT StudentID, RoomID, StudentNumber, Name, LastName, Age, PhoneNumber, Class FROM Student ORDER BY LastName;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = ReadStudent(reader);
                            students.Add(student);
                        }
                    }
                }
            }

            return students;
        }

        public Student? GetById(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT StudentID, RoomID, StudentNumber, Name, LastName, Age, PhoneNumber, Class FROM Student  WHERE StudentID = @Id;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", studentId);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var user = ReadStudent(reader);
                        return user;
                    }
                }
            }
            return null;

        }

        public void Update(Student student)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Student SET   StudentNumber = @StudentNumber,  Name = @Name,     LastName = @LastName,    Age = @Age,     PhoneNumber = @PhoneNumber,   Class = @Class WHERE StudentID = @StudentID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", student.StudentID);
                    command.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@lastName", student.LastName);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                    command.Parameters.AddWithValue("@Class", student.Class);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }
        public Student ReadStudent(SqlDataReader reader)
        {
            int studentId = (int)reader["StudentID"];
            string Name = (string)reader["Name"];
            string LastName = (string)reader["LastName"];
            int Age = (int)reader["Age"];
            string PhoneNumber = (string)reader["PhoneNumber"];
            string StudentNumber = (string)reader["StudentNumber"];
            string Class = (string)reader["Class"];
            int RoomID = (int)reader["RoomID"];


            return new Student(studentId, RoomID, StudentNumber, Name, LastName, Age, PhoneNumber, Class);
        }
    }
}

