using Application.Features.Course.Queries.GetByCode;
using Application.Features.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Course.Queries.GetById;

public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdRequest, CourseDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCourseByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<CourseDTO> Handle(GetCourseByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Courses.Get(request.CourseId);
        return _mapper.Map<CourseDTO>(result);

    }
}