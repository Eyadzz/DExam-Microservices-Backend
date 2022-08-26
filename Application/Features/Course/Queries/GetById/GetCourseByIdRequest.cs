using Application.Features.DTOs;
using MediatR;

namespace Application.Features.Course.Queries.GetById;

public class GetCourseByIdRequest : IRequest<CourseDTO>
{
    public string CourseId { get; set; }
}