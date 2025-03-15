using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using WebApplication26.Models;
using WebApplication26.Services;

namespace WebApplication26.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _iStudentService;

        public StudentsController(StudentService iStudentService)
        {
            _iStudentService = iStudentService;
        }

        [HttpGet("get")]
        public ActionResult<List<Student>> GetStudents()
        {
            return _iStudentService.GetStudents();
        }

        [HttpPost("create/{name}/{age}")]
        public ActionResult<Student> CreateStudent(string name, int age)
        {
            if (name == null || name == string.Empty) return BadRequest(new { data = "Name cannot be null or empty" });
            if (name.Length <= 6) return BadRequest(new { data = "Name length cannot be less then 6" });
            if (age == null || age <= 0) return BadRequest(new {data = "Age cannot be null or less then 0" });

            return Ok(_iStudentService.CreateStudent(name, age));
        }

        [HttpPatch("update")]
        public ActionResult<Student> UpdateStudent([FromBody] Student student)
        {
            if (student.Id == null || student.Id < 0) return BadRequest(new { data = "Id cannot be null or less then 0" });
            if (student.Name.Length <= 6) return BadRequest(new { data = "Name length cannot be less then 6" });
            if (student.Age == null || student.Age <= 0) return BadRequest(new { data = "Age cannot be null or less then 0" });

            return Ok(_iStudentService.UpdateStudent(student));
        }

        [HttpDelete("delete/{studentId}")]
        public ActionResult<bool> DeleteStudent(int studentId)
        {
            if (studentId == null || studentId < 0) return BadRequest(new { data = "Id cannot be null or less then 0" });

            return Ok(_iStudentService.DeleteStudent(studentId));
        }
    }
}
