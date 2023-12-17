using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domaine;

namespace DataAccesLayer
{
   public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();

        void addCourse(Course course);
        void UpdateCourse(int courseId, Course updatedCourse);
        void DeleteCourse(int courseId);
    }
}
