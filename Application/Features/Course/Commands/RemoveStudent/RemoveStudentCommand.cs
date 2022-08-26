using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Features.Course.Commands.RemoveStudent;

public class RemoveStudentCommand : IRequest<string>
{
   [Required] public string? StudentId { get; set; }
   [Required] public string? CourseId { get; set; }
}