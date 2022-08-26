using Application.Features.Course.Queries.GetByCode;
using Application.Features.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Course.Queries.GetByTeacherId;

public class GetCourseByTeacherHandler : IRequestHandler<GetCourseByTeacherRequest, List<CourseDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCourseByTeacherHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<CourseDTO>> Handle(GetCourseByTeacherRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Courses.FindBy(c => c.TeacherId == request.TeacherId);
        return _mapper.Map<List<CourseDTO>>(result);

    }
}