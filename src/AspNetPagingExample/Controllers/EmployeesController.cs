using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AspNetPagingExample.Models;
using AspNetPagingExample.Models.Entities;

namespace AspNetPagingExample.Controllers
{
    public class EmployeesController : BaseController
    {
        public async Task<IHttpActionResult> Get(int? page = null, int pageSize = 10, string orderBy = nameof(Employee.Id), bool ascending = true)
        {
            if (page == null)
                return Ok(await EntityContext.Employees.ToListAsync());

            var employees = await CreatePagedResults<Employee, EmployeeModel>
                (EntityContext.Employees, page.Value, pageSize, orderBy, ascending);
            return Ok(employees);
        }

        public EmployeesController(EntityContext context) : base(context) {}
    }
}