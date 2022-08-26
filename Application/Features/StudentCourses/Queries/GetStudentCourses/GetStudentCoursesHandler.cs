using Application.Features.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.StudentCourses.Queries.GetStudentCourses;

public class GetStudentCoursesHandler : IRequestHandler<GetStudentCoursesRequest, ICollection<CourseDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetStudentCoursesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ICollection<CourseDTO>> Handle(GetStudentCoursesRequest request, CancellationToken cancellationToken)
    {
        var studentCourses = await _unitOfWork.StudCrs.GetStudentCourses(request.StudentId);
        List<CourseDTO> courses = new List<CourseDTO>();
        foreach (var courseId in studentCourses!.Courses)
        {
            var course = await _unitOfWork.Courses.Get(courseId);
            courses.Add(_mapper.Map<CourseDTO>(course));
        }

        return courses;
    }
}