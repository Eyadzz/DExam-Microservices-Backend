using Application.Features.Course.Commands.Create;
using Application.Interfaces.Repositories;
using Application.Messaging;
using MediatR;

namespace Application.Features.Course.Commands.RemoveStudent;

public class RemoveStudentHandler : IRequestHandler<RemoveStudentCommand,string>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveStudentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
    {
        await RemoveStudent(request.StudentId!, request.CourseId!);
        await RemoveStudentFromCourse(request.StudentId!, request.CourseId!);
        
        return "Student was removed from course";
    }

    private async Task RemoveStudent(string studentId, string courseId)
    {
        var sc = await _unitOfWork.StudCrs.GetStudentCourses(studentId);
        if (sc == null)
            throw new Exception("Student not found");
        
        sc.Courses.Remove(courseId);
        await _unitOfWork.StudCrs.Update(sc);
    }

    private async Task RemoveStudentFromCourse(string studentId, string courseId)
    {
        var course = await _unitOfWork.Courses.Get(courseId);
        if (course == null)
            throw new Exception("Course not found");
        
        course.Students.Remove(studentId);
        await _unitOfWork.Courses.Update(course);
    }
}