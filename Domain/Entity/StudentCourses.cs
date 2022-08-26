using Core.Entity.Common;
using Redis.OM.Modeling;

namespace Core.Entity;

[Document(StorageType = StorageType.Json, Prefixes = new []{"StudentCourses"})]
public class StudentCourses : BaseEntity
{
    public StudentCourses(string studentId)
    {
        Courses = new List<string>();
        CreatedAt = DateTime.Now.ToString();
        StudentId = studentId;
    }
    [RedisIdField] [Indexed] public string StudentId { get; set; }
    [Indexed] public List<string> Courses { get; set; } = null!;
}