namespace RealEstate.Core.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }

        IRoleRepository Role { get; }
    }
}
