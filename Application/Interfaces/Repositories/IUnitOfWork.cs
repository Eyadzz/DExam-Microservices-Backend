using Application.Interfaces.Repository;

namespace Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    public ICourseRepository Courses { get; }
    public IStudCrsRepository StudCrs { get; }
}