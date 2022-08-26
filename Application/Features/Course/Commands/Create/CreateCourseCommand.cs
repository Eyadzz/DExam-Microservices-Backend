using System.ComponentModel.DataAnnotations;
using Application.Features.DTOs;
using MediatR;

namespace Application.Features.Course.Commands.Create;

public class CreateCourseCommand : IRequest<CourseDTO>
{
  [Required] public string Name { get; set; }
  [Required] public string TeacherId { get; set; }
  [Required] public DateTime StartDate { get; set; }
  [Required] public DateTime EndDate { get; set; }
}