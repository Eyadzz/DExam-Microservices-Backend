using Application.Features.Course.Commands.Create;
using Application.Interfaces.Repositories;
using Application.Messaging;
using MediatR;

namespace Application.Features.Course.Commands.Delete;

public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand,string>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Courses.Get(request.CourseId);
        
        if (course == null)
        {
            throw new Exception("Cannot find course with given id");
        }

        await RemoveStudentsFromCourse(request.CourseId);
        DeleteCourse(request.CourseId);
        RabbitMq.Send(request.CourseId);

        return "Successfully Deleted";
    }

    private void DeleteCourse(string courseId)
    {
        _unitOfWork.Courses.Delete(courseId);
    }
    
    private async Task RemoveStudentsFromCourse(string courseId)
    {
        var registeredStudents = await _unitOfWork.StudCrs.FindBy(studentCourses => studentCourses.Courses.Contains(courseId));
        foreach (var student in registeredStudents)
        {
            student.Courses.Remove(courseId);
            await _unitOfWork.StudCrs.Update(student);
        }
    }
}