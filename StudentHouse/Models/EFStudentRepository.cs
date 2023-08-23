using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHouse.Models
{
    public class EFStudentRepository : IStudentRepository
    {
        public ApplicationDbContext context;
        public EFStudentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Student> Students => context.Students;

        public bool AddStudent(Student student)
        {
            foreach (Student s in context.Students)
            {
                if (s.Email == student.Email) { return false; }
            }
            context.Students.Add(student);
            return context.SaveChanges() > 0;
        }
    }
}
