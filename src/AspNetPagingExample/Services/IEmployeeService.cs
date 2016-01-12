using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AspNetPagingExample.Models.Entities;

namespace AspNetPagingExample.Services
{
    public interface IEmployeeService
    {
        Task<IQueryable<Employee>> GetEmployees();
    }

    public class EmployeeService
    {
        
    }
}