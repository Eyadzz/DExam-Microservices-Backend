using Application.Features.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repository;
using Application.Utility;
using AutoMapper;
using MediatR;

namespace Application.Features.Course.Commands.Create;

public class CreateCourseHandler : IRequestHandler<CreateCourseCommand,CourseDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCourseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<CourseDTO> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = _mapper.Map<Core.Entity.Course>(request);
        course.Code = CodeGenerator.Generate();
        var result =  await _unitOfWork.Courses.Create(course);
        
        return _mapper.Map<CourseDTO>(result);
    }
}