namespace PlatformService.Data.Repository;

public interface IPlatformRepo<T> : IRepo<T>
    where T : class
{
}
