using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetPagingExample.Models.Entities;
using AutoMapper.Attributes;

namespace AspNetPagingExample.Models
{
    [MapsFrom(typeof(Employee))]
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<TodoModel> TodoList { get; set; }
    }
}