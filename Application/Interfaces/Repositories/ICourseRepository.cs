using System.Linq.Expressions;
using Core.Entity;

namespace Application.Interfaces.Repository;

public interface ICourseRepository
{
    Task<Course?> Create(Course? course);
    Task<Course?> Update(Course? updatedCourse);
    void Delete(string id);
    Task<Course?> Get(string id);
    Task<IList<Course?>> FindBy(Expression<Func<Course?, bool>> expression);
    Task<Course?> FindBySingle(Expression<Func<Course?, bool>> expression);
}