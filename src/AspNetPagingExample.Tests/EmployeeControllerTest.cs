using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using AspNetPagingExample.Controllers;
using AspNetPagingExample.Models;
using AspNetPagingExample.Models.Entities;
using Moq.EntityFramework;
using NUnit.Framework;

namespace AspNetPagingExample.Tests
{
    [TestFixture]
    public class EmployeeControllerTest
    {
        public EmployeesController Controller { get; private set; }

        public DbContextMock<EntityContext> ContextMock { get; private set; }

        [SetUp]
        public void Setup()
        {
            ModelMapper.Init();
            ContextMock = DbContextMockFactory.Create<EntityContext>()
                                .MockSetFor(GetEmployees());
            Controller = new EmployeesController(ContextMock.Object);
        }

        [Test]
        public async Task TestTotalPageCount()
        {
            var page = await GetPagedEmployeeResult(1, 15);
            Assert.That(page.Content.TotalNumberOfPages, Is.EqualTo(7));
        }

        [Test]
        public async Task TestTotalPageCountWithModOfZero()
        {
            var page = await GetPagedEmployeeResult(3, 10);
            Assert.That(page.Content.TotalNumberOfPages, Is.EqualTo(10));
        }

        [Test]
        public async Task TestPageSize()
        {
            var page = await GetPagedEmployeeResult(1, 15);
            Assert.That(page.Content.PageSize, Is.EqualTo(15));
        }

        [Test]
        public async Task TestTotalNumberOfRecords()
        {
            var page = await GetPagedEmployeeResult(1, 15);
            Assert.That(page.Content.TotalNumberOfRecords, Is.EqualTo(100));
        }

        [Test]
        public async Task TestPageNumber()
        {
            var page = await GetPagedEmployeeResult(3, 15);
            Assert.That(page.Content.PageNumber, Is.EqualTo(3));
        }

        [Test]
        public async Task TestOrderBy()
        {
            var page = await GetPagedEmployeeResult(1, orderBy:"FirstName", ascending:false);
            Assert.That(page.Content.Results.First().FirstName, Is.EqualTo("First99"));
        }

        private async Task<OkNegotiatedContentResult<PagedResults<EmployeeModel>>> GetPagedEmployeeResult(int page, int pageSize = 10, string orderBy = nameof(Employee.Id), bool ascending = true)
        {
            var get = await Controller.Get(page, pageSize, orderBy, ascending);
            Assert.IsInstanceOf<OkNegotiatedContentResult<PagedResults<EmployeeModel>>>(get);

            return (OkNegotiatedContentResult<PagedResults<EmployeeModel>>) get;
        }

        private static IEnumerable<Employee> GetEmployees()
        {
            return Enumerable.Range(1, 100).Select(i => new Employee {
                Id = i,
                FirstName = "First" + i,
                LastName = "Last" + i
            });
        }
    }
}
