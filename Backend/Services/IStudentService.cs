using WebApplication26.Models;

namespace WebApplication26.Services
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        Student CreateStudent(string name, int age);
        Student UpdateStudent(Student student);
        bool DeleteStudent(int studentId);
    }
}
