using MediatR;

namespace Application.Features.Course.Commands.Delete;

public class DeleteCourseCommand : IRequest<string>
{
    public string CourseId { get; set; }
}