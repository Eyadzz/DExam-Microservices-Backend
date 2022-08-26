using Application.Features.DTOs;
using MediatR;

namespace Application.Features.Course.Queries.GetByCode;

public class GetCourseByCodeRequest : IRequest<CourseDTO>
{
    public string CourseCode { get; set; }
}