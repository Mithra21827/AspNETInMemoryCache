using AspNETInMemoryCache.Services;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AspNETInMemoryCache.Infrastructure
{

    public class EmployeeServices : IEmployeeServices<Employee, int>
    {
        readonly EmployeeContext _context;

        public EmployeeServices(EmployeeContext context)
        {
            _context = context;
        }
        public  async Task<Employee> AddEmpoyee(Employee Entity)
        {
            var model = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e=> e.EmployeeId == Entity.EmployeeId);
            if (model == null) {
                _context.Employees.Add(Entity);
                await _context.SaveChangesAsync();
                return Entity;
            }
            return model;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var model =await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (model != null)
            {
                _context.Employees.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Employee> GetEmployById(int id)
        {
            return await  _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async  Task<List<Employee>> GetEmployees()
        {
            return  await _context.Employees.AsNoTracking().ToListAsync();

        }

        public async Task<bool> UpdateEmployee(Employee Entity)
        {
           var model = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e=> e.EmployeeId == Entity.EmployeeId);
            if (model != null) {
                _context.Employees.Update(Entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
