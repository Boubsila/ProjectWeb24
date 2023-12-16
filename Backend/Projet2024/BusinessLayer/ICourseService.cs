using Domaine;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer
{
    public interface ICourseService
    {
        IEnumerable< Course> GetAll();
        void addCourse(Course course);
       Course GetCourseById(int Id);
    }
}