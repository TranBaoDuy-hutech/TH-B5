using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_B5.DAL.Entitys;

namespace TH_B5.BUS
{
    public class MajorService
    {
        public List<Major> GetAllByFaculty(int facultyId)
        {
            StudentModel context = new StudentModel();
            return context.Major.Where(p=>p.FaculyID == facultyId).ToList();
        }
    }
}
