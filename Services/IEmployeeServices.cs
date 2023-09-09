using Models;

namespace AspNETInMemoryCache.Services
{
    public interface IEmployeeServices<TEntity, TIdentity>
    {
        Task<List<TEntity>> GetEmployees();
        Task<TEntity> GetEmployById( TIdentity id);
        Task<bool> UpdateEmployee( TEntity Entity);
        Task<bool> DeleteEmployee( TIdentity id);
        Task<TEntity> AddEmpoyee(TEntity Entity);
    }
}
