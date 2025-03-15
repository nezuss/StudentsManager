using System.Data.SqlClient;
using WebApplication26.Models;

namespace WebApplication26.Services
{
    public class StudentService : IStudentService
    {

        private readonly string _connectionString;

        public StudentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public List<Student> GetStudents()
        {
            var students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name, Age FROM Students";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Age = reader.GetInt32(2)

                            });
                        }
                }
            }
            return students;
        }

        public Student CreateStudent(string name, int age)
        {
            int id = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT MAX(Id) FROM Students";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                            if (reader.Read())
                            {
                                if (reader.IsDBNull(0)) id = 0;
                                else id = reader.GetInt32(0) + 1;
                            }
                    }

                    query = $"INSERT INTO Students (Id, Name, Age) VALUES ({id}, '{name}', {age})";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return new Student { Id = id, Name = name, Age = age };
        }

        public Student UpdateStudent(Student student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT MAX(Id) FROM Students";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                            if (reader.Read()) if (reader.GetInt32(0) < student.Id) return null;
                    }

                    query = $"UPDATE Students SET Name = '{student.Name}', Age = {student.Age} WHERE Id = {student.Id}";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return student;
        }

        public bool DeleteStudent(int studentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT MAX(Id) FROM Students";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                            if (reader.Read()) if (reader.GetInt32(0) < studentId) return false;
                    }

                    query = $"SELECT 1 FROM Students WHERE Id = {studentId}";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                            if (!reader.Read()) return false;
                    }

                    query = $"DELETE FROM Students WHERE Id = {studentId}";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }

            return false;
        }
    }
}