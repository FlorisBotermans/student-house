using System.Linq;

namespace StudentHouse.Models
{
    public interface IStudentRepository
    {
        IQueryable<Student> Students { get; }
        bool AddStudent(Student student);
    }
}
