using MediatR;

namespace Application.Features.StudentCourses.Commands.CreateStudentCourses;

public class CreateStudentCoursesRequest : IRequest<string>
{
    public string StudentId { get; set; }
}