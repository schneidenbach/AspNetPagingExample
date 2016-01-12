using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetPagingExample.Models.Entities
{
    public class Employee : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Todo> TodoList { get; set; } = new List<Todo>();
    }
}