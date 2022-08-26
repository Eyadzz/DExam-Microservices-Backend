using Application.Features.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Course.Queries.GetByCode;

public class GetCourseByCodeHandler : IRequestHandler<GetCourseByCodeRequest, CourseDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCourseByCodeHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<CourseDTO> Handle(GetCourseByCodeRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Courses.FindBySingle(c => c.Code == request.CourseCode);
        return _mapper.Map<CourseDTO>(result);

    }
}