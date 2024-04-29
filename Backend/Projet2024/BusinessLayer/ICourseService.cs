using Domaine;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer
{
    public interface ICourseService
    {
        IEnumerable< Course> GetAll();
        void addCourse(Course course);
        void DeleteCourse(int courseId);
        void UpdateCourse(int courseId, Course updatedCourse);


    }
}