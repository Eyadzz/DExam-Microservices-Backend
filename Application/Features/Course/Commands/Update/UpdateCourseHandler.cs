using Application.Features.Course.Commands.Update;
using Application.Features.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repository;
using Application.Utility;
using AutoMapper;
using MediatR;

namespace Application.Features.Course.Commands.Create;

public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand,CourseDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCourseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CourseDTO> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Courses.Get(request.CourseId);
        
        if (course == null)
        {
            throw new Exception("Cannot find course with given id");
        }
        
        course.Name = request.NewCourseName;
        var result = await _unitOfWork.Courses.Update(course);
        
        return _mapper.Map<CourseDTO>(result);
    }
}