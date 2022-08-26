using Application.Features.DTOs;
using MediatR;

namespace Application.Features.Course.Queries.GetByTeacherId;

public class GetCourseByTeacherRequest : IRequest<List<CourseDTO>>
{
    public string TeacherId { get; set; }
}