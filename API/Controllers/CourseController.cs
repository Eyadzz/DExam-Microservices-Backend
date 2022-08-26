using Application.Features.Course.Commands.Create;
using Application.Features.Course.Commands.Delete;
using Application.Features.Course.Commands.Update;
using Application.Features.Course.Queries.GetByCode;
using Application.Features.Course.Queries.GetById;
using Application.Features.Course.Queries.GetByTeacherId;
using Application.Features.StudentCourses.Commands.CreateStudentCourses;
using Application.Features.StudentCourses.Queries.GetStudentCourses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/courses/")]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(CreateCourseCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet]
    [Route("get-by-id")]
    public async Task<IActionResult> GetById(string courseId)
    {
        var result = await _mediator.Send(new GetCourseByIdRequest{CourseId = courseId});
        return Ok(result);
    }

    [HttpGet]
    [Route("get-by-code")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var result = await _mediator.Send(new GetCourseByCodeRequest{CourseCode = code});
        return Ok(result);
    }
    
    [HttpGet]
    [Route("get-by-teacher")]
    public async Task<IActionResult> GetByTeacher(string id)
    {
        var result = await _mediator.Send(new GetCourseByTeacherRequest(){TeacherId = id});
        return Ok(result);
    }
    
    [HttpPatch]
    [Route("update")]
    public async Task<IActionResult> Update(UpdateCourseCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(string? courseId)
    {
        var result = await _mediator.Send(new DeleteCourseCommand {CourseId = courseId});
        return Ok(result);
    }

    [HttpPost]
    [Route("register-student")]
    public async Task<IActionResult> RegisterStudent(RegisterStudentCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpDelete]
    [Route("remove-student")]
    public async Task<IActionResult> RemoveStudent(RegisterStudentCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("get-student-courses")]
    public async Task<IActionResult> GetStudentCourses(string studentId)
    {
        var result = await _mediator.Send(new GetStudentCoursesRequest() {StudentId = studentId});
        return Ok(result);
    }
    
    [HttpPost]
    [Route("create-student-courses")]
    public async Task<IActionResult> CreateStudentCourses(string studentId)
    {
        var result = await _mediator.Send(new CreateStudentCoursesRequest() {StudentId = studentId});
        return Ok(result);
    }
}