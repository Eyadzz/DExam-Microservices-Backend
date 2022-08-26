using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.StudentCourses.Commands.CreateStudentCourses;

public class CreateStudentCoursesHandler : IRequestHandler<CreateStudentCoursesRequest,string>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateStudentCoursesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<string> Handle(CreateStudentCoursesRequest request, CancellationToken cancellationToken)
    {
        Core.Entity.StudentCourses studentCourses = new Core.Entity.StudentCourses(request.StudentId);
        await _unitOfWork.StudCrs.Create(studentCourses);
        return "Success";
    }
}