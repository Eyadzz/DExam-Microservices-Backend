using Application.Features.DTOs;
using MediatR;

namespace Application.Features.StudentCourses.Queries.GetStudentCourses;

public class GetStudentCoursesRequest : IRequest<ICollection<CourseDTO>>
{
    public string StudentId { get; set; }
}