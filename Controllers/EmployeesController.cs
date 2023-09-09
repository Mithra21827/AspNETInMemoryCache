using AspNETInMemoryCache.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Runtime.CompilerServices;

namespace AspNETInMemoryCache.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        readonly IEmployeeServices<Employee,int> _employeeServices;
        readonly ICacheService _cacheService;
        public EmployeesController(IEmployeeServices<Employee, int> employeeServices, ICacheService cacheService)
        {
            _employeeServices = employeeServices;
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees() { 
            var model = await _employeeServices.GetEmployees();
            if (model != null) {
                return Ok(model);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id) {
            if (id > 0) {

                var model = _cacheService.GetData<Employee>("employee");
                if (model != null)
                {
                    Console.WriteLine("from Cache");
                    return Ok(model);

                }
                else
                {
                    model = await _employeeServices.GetEmployById(id);
                    Console.WriteLine("from Database");

                    var exptime = DateTime.Now.AddSeconds(30);
                    var _isSet = _cacheService.SetData<Employee>("employee", model, exptime);
                    return Ok(model);    
                }
            }
            return BadRequest("Id should not be less than 0");
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee) {
            if (employee != null) { 
                var model = await _employeeServices.AddEmpoyee(employee);
                return Ok(model);   
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id) {
            if (id > 0) { 
                var model = await _employeeServices.DeleteEmployee(id);
                if (model) {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();

        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            if (employee != null)
            {
                var model = await _employeeServices.UpdateEmployee(employee);
                if (model)
                {
                    return Ok(employee);
                }
                return NotFound(model);
            }
            return BadRequest();
        }
    }
}
