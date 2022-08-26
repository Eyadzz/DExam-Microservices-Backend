using System.ComponentModel.DataAnnotations;
using Application.Features.DTOs;
using MediatR;

namespace Application.Features.Course.Commands.Create;

public class RegisterStudentCommand : IRequest<string>
{
   [Required] public string? StudentId { get; set; }
   [Required] public string? CourseCode { get; set; }
}