using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_B5.DAL.Entitys;

namespace TH_B5.BUS
{
    public class FacultyService
    {
        public List<Faculty> GetAll()
        {
            StudentModel context = new StudentModel();
            return context.Faculty.ToList();
        }
    }
}
