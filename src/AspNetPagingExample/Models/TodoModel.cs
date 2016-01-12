using AspNetPagingExample.Models.Entities;
using AutoMapper.Attributes;

namespace AspNetPagingExample.Models
{
    [MapsFrom(typeof (Todo))]
    public class TodoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}