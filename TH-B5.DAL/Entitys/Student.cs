namespace TH_B5.DAL.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentID { get; set; }

        [StringLength(100)]
        public string Avartar { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public decimal AverageScore { get; set; }

        public int? FacultyID { get; set; }

        public int? MajorID { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual Major Major { get; set; }
    
    }
    public class StudentRepository
    {
        private readonly StudentModel context;

        public StudentRepository()
        {
            context = new StudentModel();
        }

        public void Add(Student student)
        {
            context.Student.Add(student);
            context.SaveChanges();
        }

        public void Update(Student student)
        {
            var existingStudent = context.Student.Find(student.StudentID);
            if (existingStudent != null)
            {
                existingStudent.FullName = student.FullName;
                existingStudent.FacultyID = student.FacultyID;
                existingStudent.AverageScore = student.AverageScore;
                existingStudent.MajorID = student.MajorID;
                //existingStudent.Avatar = student.Avatar;
                context.SaveChanges();
            }
        }

        public void Delete(int studentId)
        {
            var student = context.Student.Find(studentId);
            if (student != null)
            {
                context.Student.Remove(student);
                context.SaveChanges();
            }
        }
    }
}
