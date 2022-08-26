using Application.Features.Course.Commands.Create;
using Application.Features.DTOs;
using Application.Utility;
using AutoMapper;
using Core.Entity;

namespace Application.Profiles;

public class ProfilesMapper : Profile
{
    public ProfilesMapper()
    {
        CreateMap<Course, CourseDTO>().ReverseMap();
        
        CreateMap<Course, CreateCourseCommand>().ReverseMap();
    }
}