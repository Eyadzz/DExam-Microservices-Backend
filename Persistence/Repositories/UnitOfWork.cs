using Application.Interfaces.Repositories;
using Application.Interfaces.Repository;
using Redis.OM;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly RedisConnectionProvider _provider;

    public UnitOfWork(RedisConnectionProvider provider)
    {
        _provider = provider;
        Courses = new CourseRepository(_provider);
        StudCrs = new StudCrsRepository(_provider);
    }

    public ICourseRepository Courses { get; }
    public IStudCrsRepository StudCrs { get; }
}