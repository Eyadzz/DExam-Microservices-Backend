using System.ComponentModel.DataAnnotations;
using Application.Features.DTOs;
using MediatR;

namespace Application.Features.Course.Commands.Update;

public class UpdateCourseCommand : IRequest<CourseDTO>
{
   [Required] public string CourseId { get; set; }
   [Required] public string? NewCourseName { get; set; }
}