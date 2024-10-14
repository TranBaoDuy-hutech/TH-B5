using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_B5.DAL.Entitys;

namespace TH_B5.BUS
{
    public class StudentService
    {
        public List<Student> GetAll()
        {
            StudentModel context = new StudentModel();
            return context.Student.ToList();
        }
        public List<Student> GetAllHasNoMajor()
        {
            StudentModel context = new StudentModel();
            return context.Student.Where(p => p.MajorID == null).ToList();
        }
        public List<Student> GetAllHasNoMajor(int facultyID)
        {
            StudentModel context = new StudentModel();
            return context.Student.Where(p => p.MajorID == null && p.FacultyID==facultyID).ToList();
        }
       
        
            private readonly StudentRepository studentRepository;

            public StudentService()
            {
                studentRepository = new StudentRepository();
            }

            public void AddStudent(Student student)
            {
                studentRepository.Add(student);
            }

            public void UpdateStudent(Student student)
            {
                studentRepository.Update(student);
            }

            public void DeleteStudent(int studentId)
            {
                studentRepository.Delete(studentId);
            }

    }
}
