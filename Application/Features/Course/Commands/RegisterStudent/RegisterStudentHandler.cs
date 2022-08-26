using Application.Features.Course.Commands.Create;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Course.Commands.RegisterStudent;

public class RegisterStudentHandler : IRequestHandler<RegisterStudentCommand,string>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterStudentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Courses.FindBySingle(c => c.Code == request.CourseCode);
        if (course == null)
        {
              throw new Exception("Course not found");
        }
        
        var studentCourses = await _unitOfWork.StudCrs.GetStudentCourses(request.StudentId);
        if(studentCourses==null)
        {
            throw new Exception("Student not found");
        }
        /*if (studentCourses.Any(sc => sc.CourseId == course.Id))
        {
            throw new Exception("Student already registered for this course");
        }*/
        //TODO: check if student is already registered in the course
        //TODO: add begin transaction and commit for unit of work
        course.Students.Add(request.StudentId);
        await _unitOfWork.Courses.Update(course);

        
        studentCourses.Courses.Add(course.CourseId.ToString());
        await _unitOfWork.StudCrs.Update(studentCourses);
        
        return "Student registered successfully";
    }
}